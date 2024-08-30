using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTOS;
using ProductService.Extensions;

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
            IEnumerable<CategoryReadDTO> categories = (await _context.Categories.AsNoTracking().ToListAsync()).ToCategoryReadDTOList();
            return Ok(categories);
        }
    }
}
