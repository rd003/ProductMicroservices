using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTOS;

namespace ProductService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductContext _context;

        public CategoriesController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            IEnumerable<CategoryPublishDTO> categories = await _context.Categories.Select(a => new CategoryPublishDTO
            {
                Id = a.Id,
                Name = a.Name
            }).ToListAsync();
            return Ok(categories);
        }
    }
}
