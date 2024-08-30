using ProductService.Models;

namespace ProductService.Data;

public static class DbSeeder
{
    public static void Seed(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<ProductContext>();

        // TODO: Remove this after testing
        if (context!.Categories.Any() == false)
        {
            List<Category> categories = [
                new Category {Name="Fruits"},
                new Category {Name="Vegetables"},
                new Category {Name="Spices"},
            ];
            context.Categories.AddRange(categories);
            context.SaveChanges();
            Console.WriteLine("======> categories seeded");

        }
        else
        {
            Console.WriteLine("======> Already have data");
        }
    }
}
