using FirstApiProject.DAL.EFCore;
using FirstApiProject.Entities.Dtos.Cars;
using FirstApiProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using FirstApiProject.Entities.Dtos.Brands;

namespace FirstApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        private readonly AppDbContext _context;

        public BrandsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.brands.ToListAsync();
            if (result.Count == 0) return NotFound();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpGet("GetBrand/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _context.brands.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (result is null) return NotFound();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateBrandDto branddto)
        {
            var result = await _context.brands.FindAsync(id);
            if (result is null) return NotFound();
            result.Name = branddto.Name;
          
          await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.brands.FindAsync(id);
            if (result is null) return BadRequest(new
            {
                StatusCode = 201,
                Message = "Tapilmadi"
            });
            _context.brands.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateBrandDto branddto)
        {

            Brand brand = new Brand
            {
                Name = branddto.Name,
             


            };
            await _context.brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}

    

