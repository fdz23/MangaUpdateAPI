using MangaUpdateAPI.DAO;
using MangaUpdateAPI.DTO;
using MangaUpdateAPI.Model;
using MangaUpdateAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace MangaUpdateAPI.Controllers
{
    [Route("/Manga")]
    [ApiController]
    public class MangaController : Controller
    {
        [HttpGet("{id}")]
        public ActionResult<List<Manga>> GetMangaFromUserWeb(int id)
        {
            try
            {
                return Accepted(GetMangaFromUser(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        public ActionResult CreateMangaForUser(MangaUserDTO mangaUser)
        {
            var userDao = new UserDAO();
            var user = userDao.GetById(mangaUser.UserId);

            if (user == null)
                return NotFound();

            var mangaDao = new MangaDAO();
            mangaDao.CreateMangaForUser(mangaUser.Manga, user);

            return Accepted();
        }

        [HttpGet("ToRead/{id}")]
        public async Task<ActionResult<List<Manga>>> GetChaptersToRead(int id)
        {
            try
            {
                HttpClient client = new HttpClient();

                var mangas = GetMangaFromUser(id);
                var errors = new List<string>();
                var mangasToRead = new List<Manga>();

                foreach (var manga in mangas)
                {
                    if (!manga.HasReadLatestChapter())
                    {
                        mangasToRead.Add(manga);
                    }
                    else
                    {
                        var url = UrlHelper.GetNextChapterUrl(manga);
                        var request = HttpHelper.GetValidRequest();
                        request.RequestUri = new Uri(url);
                        var result = await client.SendAsync(request);

                        if (result.IsSuccessStatusCode)
                        {
                            mangasToRead.Add(manga);

                            manga.Chapter = manga.ObtainNextChapter();
                            manga.Url = url;

                            var mangaDao = new MangaDAO();
                            mangaDao.Update(manga);
                        }
                        
                        if (result.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            errors.Add("Manga site forbids requests from applications!");
                        }
                    }
                }

                return Accepted(mangasToRead);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        private List<Manga> GetMangaFromUser(int id)
        {
            var userDao = new UserDAO();
            var user = userDao.GetById(id);

            if (user == null)
                throw new Exception("User not found");

            var mangaDao = new MangaDAO();
            var mangas = mangaDao.GetMangaFromUser(user);

            return mangas;
        }
    }
}
