using MangaUpdateAPI.DAO;
using MangaUpdateAPI.DTO;
using MangaUpdateAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace MangaUpdateAPI.Controllers
{
    [Route("/User")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost("Register")]
        public ActionResult<Model.User> Register(RegisterDTO info)
        {
            var user = new User() { Name = info.Name };
            var identity = new Identity() { Username = info.Username, Password = info.Password };

            user = new UserDAO().CreateNewUser(user, identity);

            if (user == null)
                return BadRequest();

            return Accepted(user);
        }

        [HttpPost("Login")]
        public ActionResult<Model.User> Login(LoginDTO info)
        {
            var user = new UserDAO().ValidateCredentials(info.Username, info.Password);

            if (user == null)
                return NotFound();

            return Accepted(user);
        }
    }
}
