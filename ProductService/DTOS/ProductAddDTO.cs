using System;
using System.ComponentModel.DataAnnotations;

namespace ProductService.DTOS;

public class ProductAddDTO
{
    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
