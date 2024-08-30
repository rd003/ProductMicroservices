using ProductService.DTOS;
using ProductService.Models;

namespace ProductService.Extensions;

public static class Extension
{

    // convert ProductAddDTO to Product
    public static Product ToProduct(this ProductAddDTO prdoductAddDTO)
    {
        return new Product
        {
            CategoryId = prdoductAddDTO.CategoryId,
            Name = prdoductAddDTO.Name,
            Description = prdoductAddDTO.Description,
            Price = prdoductAddDTO.Price
        };
    }

    // convert Product to ProductReadDTO
    public static ProductReadDTO ToProductReadDTO(this Product product)
    {
        return new ProductReadDTO
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }

    // Convert ProductUpdateDTO to Product
    public static Product ToProduct(this ProductUpdateDTO productUpdateDTO)
    {
        return new Product
        {
            Id = productUpdateDTO.Id,
            CategoryId = productUpdateDTO.CategoryId,
            Name = productUpdateDTO.Name,
            Description = productUpdateDTO.Description,
            Price = productUpdateDTO.Price
        };
    }

    // // convert Category to CategoryPublishDTO

    public static CategoryPublishDTO ToProductReadDTO(this Category category)
    {
        return new CategoryPublishDTO
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
