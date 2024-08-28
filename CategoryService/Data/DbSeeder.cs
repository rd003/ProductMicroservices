using System.Text.Json;
using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Data;

public static class DbSeeder
{
    public static async Task SeedData(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<CategoryContext>();
        Console.WriteLine("===>Seeding data");

        try
        {
            if (await context!.Categories.AnyAsync())
            {
                Console.WriteLine("===>Already have data");
                return;
            }
            string filePath = "Data/Seed/categories.json";
            string jsonString = await File.ReadAllTextAsync(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var categories = JsonSerializer.Deserialize<List<Category>>(jsonString, options);
            if (categories != null)
            {
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException);
        }
    }
}
