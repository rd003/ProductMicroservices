using ProductService.Models;
using ProductService.SyncService.Grpc;

namespace ProductService.Data;

public static class DbSeeder
{
    public static async Task Seed(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var grpcClient = scope.ServiceProvider.GetService<ICategoryDataClient>();
        var categories = grpcClient.GetAllCategories();

        await SeedData(scope.ServiceProvider.GetService<ProductContext>(), categories);
    }

    private static async Task SeedData(ProductContext? productContext, IEnumerable<Category> categories)
    {
        if (productContext == null) { return; }
        Console.WriteLine("Seeding new categories...");
        try
        {
            productContext.AddRange(categories);
            await productContext.SaveChangesAsync();
            Console.WriteLine("New categories seeded.");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("===> could not seed data");
        }

    }
}
