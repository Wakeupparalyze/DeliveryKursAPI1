using ConsoleApp1;
using DeliveryKursAPI1.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryKursAPI1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DeliveryKurs1Context _context;
        public UserController(DeliveryKurs1Context context)
        {
            _context = context;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            List<UserDTO> users = _context.Users.ToList().Select(s => new UserDTO
            {
                Id = s.Id,
                Login = s.Login,
                Password = s.Password,
                Name = s.Name,
                Lastname = s.Lastname
            }).ToList();
            return users;
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var s = _context.Users.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(new UserDTO
            {
                Id = s.Id,
                Login = s.Login,
                Password = s.Password,
                Name = s.Name,
                Lastname = s.Lastname,
            });
        }

        [HttpPost("UserLogin")]
        public ActionResult<UserDTO> UserLogin(LoginUser loginUser)
        {

            User user = _context.Users.FirstOrDefault(a => a.Login == loginUser.Login && a.Password == loginUser.Password);
            if (user != null)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name,
                    Lastname = user.Lastname,
                };
            }
            else
            {
                return BadRequest("Неправильный логин или пароль");
            }

        }
        [HttpPost("UserRegistration")]
        public ActionResult<User> Registration(User user)
        {
            if (_context.Users.Any(u => u.Login == user.Login))
            {
                return BadRequest("Пользователь с таким никнеймом уже зарегистрирован");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Пользователь зарегистрирован");
        }
    }
}
