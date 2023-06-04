using FirstApiProject.DAL.EFCore;
using FirstApiProject.Entities;
using FirstApiProject.Entities.Dtos.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FirstApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result=await _context.cars.ToListAsync();
            if(result.Count==0)return NotFound();
            return StatusCode((int)HttpStatusCode.OK,result);
        }
       
        [HttpGet("GetCar/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _context.cars.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (result is null) return NotFound();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCarDto cardto)
        {
            var result = await _context.cars.FindAsync(id);
            if (result is null) return NotFound();
            result.Color = cardto.Color;
            result.Description = cardto.Description;
            result.DailyPrice = cardto.DailyPrice;
            result.Brand = cardto.Brand;
            result.BrandId = cardto.BrandId;
            result.ColorId = cardto.ColorId;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.cars.FindAsync(id);
            if (result is null) return BadRequest(new
            {
                StatusCode = 201,
                Message = "Tapilmadi"
            });
            _context.cars.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCarDto cardto)
        {

            Car car = new Car
            {
                Color = cardto.Color,
                Description = cardto.Description,
                ColorId = cardto.ColorId,
                DailyPrice = cardto.DailyPrice,
                Brand = cardto.Brand,
                BrandId = cardto.BrandId,
              

            };
            await _context.cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}

