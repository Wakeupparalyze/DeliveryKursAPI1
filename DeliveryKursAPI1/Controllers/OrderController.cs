using ConsoleApp1;
using DeliveryKursAPI1.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace DeliveryKursAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DeliveryKurs1Context _context;
        //private readonly MailService mail;
        public OrderController(DeliveryKurs1Context context /*MailService mail*/)
        {
            _context = context;
            //this.mail = mail;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            List<OrderDTO> orders = _context.Orders.ToList().Select(s => new OrderDTO
            {
                Id = s.Id,
                DishId = s.DishId,
                Total = s.Total,
                Adress = s.Adress,
                UserId = s.UserId,
                CourierId = s.CourierId,
                Status = s.Status,
                Number = s.Number,
            }).ToList();
            return orders;
        }
        [HttpGet("GetOrder")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var s = _context.Orders.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(new OrderDTO
            {
                Id = s.Id,
                DishId = s.DishId,
                Total = s.Total,
                Adress = s.Adress,
                UserId = s.UserId,
                CourierId = s.CourierId,
                Status = s.Status,
                Number = s.Number,
            });
        }
        [HttpPost("AddOrder")]
        public async Task<ActionResult<OrderDTO>> AddOrder(OrderDTO createOrder)
        {

            var neworder = new Order
            {
                Id = createOrder.Id,
                DishId = createOrder.DishId,
                Total = createOrder.Total,
                Adress = createOrder.Adress,
                UserId = createOrder.UserId,
                CourierId = createOrder.CourierId,
                Status = createOrder.Status,
                Number = createOrder.Number
            };
            _context.Orders.Add(neworder);
            await _context.SaveChangesAsync();
            createOrder.Id = neworder.Id;
            return CreatedAtAction(nameof(GetOrder), new { id = neworder.Id }, createOrder);
        }
        [HttpPut("EditOrder")]
        public async Task<IActionResult> EditOrder(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Обновляем данные товара на основе полученных данных
            order.Id = orderDTO.Id;
            order.DishId = orderDTO.DishId;
            order.Total = orderDTO.Total;
            order.Adress = orderDTO.Adress;
            order.UserId = orderDTO.UserId;
            order.CourierId = orderDTO.CourierId;
            order.Status = orderDTO.Status;
            order.Number = orderDTO.Number;
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Обработка ошибки параллельного доступа или других конкурентных изменений данных
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ошибка при обновлении товара. Попробуйте еще раз." });
            }
        }
        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            Order order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
    }
}