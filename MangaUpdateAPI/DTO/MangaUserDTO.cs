using MangaUpdateAPI.Model;

namespace MangaUpdateAPI.DTO
{
    public class MangaUserDTO
    {
        public int UserId { get; set; }

        public Manga Manga { get; set; }
    }
}
