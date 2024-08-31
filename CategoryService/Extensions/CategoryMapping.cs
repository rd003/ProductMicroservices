using System;
using CategoryService.DTOS;
using CategoryService.Models;

namespace CategoryService.Extensions;

public static class CategoryMapping
{
    public static CategoryPublishedDTO ToCategoryPubhlishedDTO(this Category category)
    {
        return new CategoryPublishedDTO(category.Id, category.Name, "");
    }

    public static GrpcCategoryModel ToGrpcCategoryModel(this Category category)
    {
        return new GrpcCategoryModel
        {
            CategoryId = category.Id,
            Name = category.Name,
        };
    }
}
