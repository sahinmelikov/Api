using FirstApiProject.DAL.EFCore;
using FirstApiProject.Entities.Dtos.Brands;
using FirstApiProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using FirstApiProject.Entities.Dtos.Colors;

namespace FirstApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : Controller
    {
        private readonly AppDbContext _context;

        public ColorsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.colors.ToListAsync();
            if (result.Count == 0) return NotFound();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpGet("GetBrand/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _context.colors.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (result is null) return NotFound();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateColorDto colordto)
        {
            var result = await _context.colors.FindAsync(id);
            if (result is null) return NotFound();
            result.Name = colordto.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.colors.FindAsync(id);
            if (result is null) return BadRequest(new
            {
                StatusCode = 201,
                Message = "Tapilmadi"
            });
            _context.colors.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateColorDto colordto)
        {

            Color color = new Color
            {
                Name = colordto.Name,



            };
            await _context.colors.AddAsync(color);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
    
