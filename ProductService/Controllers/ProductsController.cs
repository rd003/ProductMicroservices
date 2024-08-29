using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTOS;
using ProductService.Exceptions;
using ProductService.Extensions;
using ProductService.Models;

namespace ProductService.Controllers;


[ApiController]
[Route("api/c/categories/{categoryId}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductContext _context;

    public ProductsController(ProductContext context)
    {
        _context = context;
    }

    // GET: api/c/categories/1/products
    [HttpGet]
    public async Task<IActionResult> GetProcutsForCategory(int categoryId)
    {
        Category? category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with id: {categoryId} not found");
        }
        IEnumerable<ProductReadDTO> products = category.Products.Select(p => new ProductReadDTO()).ToList();

        return Ok(products);
    }

    // GET: api/c/categories/1/products/1
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProcutForCategory(int categoryId, int productId)
    {
        Category? category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with id: {categoryId} not found");

        }

        ProductReadDTO? product = await _context.Products.Include(p => p.Category).Select(p => new ProductReadDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name
        }).FirstOrDefaultAsync();
        if (product == null)
        {
            throw new NotFoundException($"Prdocut with id: {productId} not found");
        }
        return Ok();
    }


    // POST: api/c/categories/1/products
    [HttpPost()]
    public async Task<IActionResult> CreateProductForCategory(int categoryId, [FromBody] ProductAddDTO productToAdd)
    {
        Category? category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with id: {categoryId} not found");
        }
        productToAdd.CategoryId = categoryId;

        Product product = productToAdd.ToProduct();

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetProcutForCategory), new { categoryId, productId = product.Id }, product);
    }

    // PUT: api/c/categories/1/products/1
    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProductForCategory(int categoryId, int productId, [FromBody] ProductUpdateDTO productToUpdate)
    {
        Category? category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with id: {categoryId} not found");
        }

        if (productId != productToUpdate.Id)
        {
            throw new BadRequestException("Product ID mismatch");
        }

        var ProductExists = await _context.Products.AnyAsync(p => p.Id == productId);
        if (!ProductExists)
        {
            throw new NotFoundException($"Product with id: {productId} not found");
        }
        Product product = productToUpdate.ToProduct();
        _context.Products.Update(product);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/c/categories/1/products/1
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProductForCategory(int categoryId, int productId)
    {
        Category? category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with id: {categoryId} not found");
        }

        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new NotFoundException($"Product with id: {productId} not found");
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok($"Product with id: {productId} deleted successfully");
    }
}
