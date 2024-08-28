using CategoryService.Data;
using CategoryService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryContext context;

        public CategoriesController(CategoryContext categoryContext)
        {
            context = categoryContext;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                context.Categories.Add(category);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category categoryToUpdate)
        {
            try
            {
                if (categoryToUpdate == null || categoryToUpdate.Id != id)
                {
                    return BadRequest();
                }
                Category? category = await context.Categories.AsNoTracking().SingleOrDefaultAsync(a => a.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                context.Categories.Update(categoryToUpdate);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                Category? category = await context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                if (category == null)
                {
                    return NotFound();
                }
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

