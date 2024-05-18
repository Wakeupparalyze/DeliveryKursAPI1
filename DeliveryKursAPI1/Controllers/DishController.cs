using ConsoleApp1;
using DeliveryKursAPI1.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace DeliveryKursAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly DeliveryKurs1Context _context;
        public DishController(DeliveryKurs1Context context)
        {
            _context = context;
        }

        [HttpGet("GetAllDishes")]
        public async Task<ActionResult<List<DishDTO>>> GetDishes()
        {
            List<DishDTO> dishes = _context.Dishes.ToList().Select(s => new DishDTO
            {
                Id = s.Id,
                Cost = s.Cost,
                Name = s.Name,
                Image = s.Image,
            }).ToList();
            return dishes;
        }
        [HttpGet("GetDish")]
        public async Task<ActionResult<DishDTO>> GetDish(int id)
        {
            var s = _context.Dishes.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(new DishDTO
            {
                Id = s.Id,
                Cost = s.Cost,
                Name = s.Name,
                Image = s.Image,
            });
        }
        [HttpPost("AddDish")]
        public async Task<ActionResult<DishDTO>> AddDish(DishDTO createDish)
        {
            Dish dish = _context.Dishes.FirstOrDefault(a => a.Name == createDish.Name);

            if (dish != null)
            {
                return BadRequest("Блюдо с таким названием уже существует");
            }
            else
            {
                var newdish = new Dish
                {
                    Id = createDish.Id,
                    Cost = createDish.Cost,
                    Name = createDish.Name,
                    Image = createDish.Image,
                };
                _context.Dishes.Add(newdish);
                await _context.SaveChangesAsync();
                createDish.Id = newdish.Id;
                return CreatedAtAction(nameof(GetDish), new { id = newdish.Id }, createDish);
            }
        }
        [HttpPut("EditDish")]
        public async Task<IActionResult> EditDish(int id, DishDTO dishDTO)
        {
            if (id != dishDTO.Id)
            {
                return BadRequest();
            }

            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            // Обновляем данные товара на основе полученных данных
            dish.Id = dishDTO.Id;
            dish.Cost = dishDTO.Cost;
            dish.Name = dishDTO.Name;
            dish.Image = dishDTO.Image;
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
        [HttpDelete("DeleteDish")]
        public IActionResult DeleteDish(int id)
        {
            Dish dish = _context.Dishes.Find(id);

            if (dish == null)
            {
                return NotFound();
            }

            _context.Dishes.Remove(dish);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
