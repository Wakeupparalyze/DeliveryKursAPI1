using ConsoleApp1;
using DeliveryKursAPI1.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryKursAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly DeliveryKurs1Context _context;
        //private readonly MailService mail;
        public CourierController(DeliveryKurs1Context context /*MailService mail*/)
        {
            _context = context;
            //this.mail = mail;
        }

        [HttpGet("GetAllCouriers")]
        public async Task<ActionResult<List<CourierDTO>>> GetCouriers()
        {
            List<CourierDTO> couriers = _context.Couriers.ToList().Select(s => new CourierDTO
            {
                Id = s.Id,
                Login = s.Login,
                Password = s.Password,
                Name = s.Name,
                Lastname = s.Lastname,
            }).ToList();
            return couriers;
        }
        [HttpGet("GetCourier")]
        public async Task<ActionResult<CourierDTO>> GetCourier(int id)
        {
            var s = _context.Couriers.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(new CourierDTO
            {
                Id = s.Id,
                Login = s.Login,
                Password = s.Password,
                Name = s.Name,
                Lastname = s.Lastname,
            });
        }

        [HttpPost("CourierLogin")]
        public ActionResult<CourierDTO> CourierLogin(LoginCourier loginCourier)
        {

            Courier courier = _context.Couriers.FirstOrDefault(a => a.Login == loginCourier.Login && a.Password == loginCourier.Password);
            if (courier != null)
            {
                return new CourierDTO
                {
                    Id = courier.Id,
                    Login = courier.Login,
                    Password = courier.Password,
                    Name = courier.Name,
                    Lastname = courier.Lastname,
                };
            }
            else
            {
                return BadRequest("Неправильный логин или пароль");
            }

        }
    }
}
