
using ProductService.Models;

namespace ProductService.SyncService.Grpc;

public interface ICategoryDataClient
{
    IEnumerable<Category> GetAllCategories();
}
