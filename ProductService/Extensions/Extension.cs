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
            Name = prdoductAddDTO.Name,
            Description = prdoductAddDTO.Description,
            Price = prdoductAddDTO.Price
        };
    }

    // Convert ProductUpdateDTO to Product
    public static Product ToProduct(this ProductUpdateDTO productUpdateDTO)
    {
        return new Product
        {
            Id = productUpdateDTO.Id,
            Name = productUpdateDTO.Name,
            Description = productUpdateDTO.Description,
            Price = productUpdateDTO.Price
        };
    }

    // // convert Prdouct to ProductReadDTO

    // public static ProductReadDTO ToProductReadDTO(this Product product)
    // {
    //     return new ProductReadDTO
    //     {
    //         Id = product.Id,
    //         Name = product.Name,
    //         Description = product.Description,
    //         Price = product.Price
    //     };
    // }
}
