using CategoryService.Data;
using CategoryService.Extensions;
using Grpc.Core;

namespace CategoryService.SyncDataServices.Grpc;

public class GrpcCategoryService : GrpcCategory.GrpcCategoryBase
{
    private readonly CategoryContext _categoryContext;

    public GrpcCategoryService(CategoryContext ctx)
    {
        _categoryContext = ctx;
    }
    public override Task<CategoryResponse> GetAllCategories(GetAllRequest request, ServerCallContext context)
    {
        var response = new CategoryResponse();
        var categories = _categoryContext.Categories.ToList();

        foreach (var category in categories)
        {
            response.Category.Add(category.ToGrpcCategoryModel());
        }
        return Task.FromResult(response);
    }

}