using ConsoleApp1;
using DeliveryKursAPI1.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryKursAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DeliveryKurs1Context _context;
        //private readonly MailService mail;
        public AdminController(DeliveryKurs1Context context /*MailService mail*/)
        {
            _context = context;
            //this.mail = mail;
        }

        [HttpGet("GetAllAdmins")]
        public async Task<ActionResult<List<AdminDTO>>> GetAdmins()
        {
            List<AdminDTO> admins = _context.Admins.ToList().Select(s => new AdminDTO
            {
                Id = s.Id,
                Login = s.Login,
                Password = s.Password,
                Name = s.Name,
                Lastname = s.Lastname,
            }).ToList();
            return admins;
        }
        [HttpGet("GetAdmin")]
        public async Task<ActionResult<AdminDTO>> GetAdmin(int id)
        {
            var s = _context.Admins.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(new AdminDTO
            {
                Id = s.Id,
                Login = s.Login,
                Password = s.Password,
                Name = s.Name,
                Lastname = s.Lastname,
            });
        }

        [HttpPost("AdminLogin")]
        public ActionResult<AdminDTO> AdminLogin(LoginAdmin loginAdmin)
        {

            Admin admin = _context.Admins.FirstOrDefault(a => a.Login == loginAdmin.Login && a.Password == loginAdmin.Password);
            if (admin != null)
            {
                return new AdminDTO
                {
                    Id = admin.Id,
                    Login = admin.Login,
                    Password = admin.Password,
                    Name = admin.Name,
                    Lastname = admin.Lastname,
                };
            }
            else
            {
                return BadRequest("Неправильный логин или пароль");
            }

        }
    }
}
