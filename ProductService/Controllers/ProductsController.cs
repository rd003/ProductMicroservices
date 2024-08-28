using Microsoft.AspNetCore.Mvc;
using ProductService.DTOS;
using ProductService.Models;

namespace ProductService.Controllers;


[ApiController]
[Route("api/c/categories/{categoryId}/[controller]")]
public class ProductsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetProcutsForCategory(int categoryId)
    {
        return Ok();
    }

    [HttpGet("{productId}")]
    public IActionResult GetProcutForCategory(int categoryId, int productId)
    {
        return Ok();
    }

    [HttpPost("{productId}")]
    public IActionResult CreateProductForCategory(int categoryId, int productId, [FromBody] ProductAddDTO productToAdd)
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult CreateProductForCategory(int categoryId, [FromBody] ProductUpdateDTO productToUpdate)
    {
        return Ok();
    }
}
