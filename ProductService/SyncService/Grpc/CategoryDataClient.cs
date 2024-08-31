using System;
using CategoryService;
using Grpc.Net.Client;
using ProductService.Extensions;
using ProductService.Models;

namespace ProductService.SyncService.Grpc;

public class CategoryDataClient : ICategoryDataClient
{
    private readonly IConfiguration _configuration;

    public CategoryDataClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Category> GetAllCategories()
    {
        Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCategory"]}");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcCategory"]);
        var client = new GrpcCategory.GrpcCategoryClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllCategories(request);
            var categories = reply.Category.Select(a => a.ToCategory()).ToList();
            return categories;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not call GRPC Server {ex.Message}");
            return null;
        }
    }
}
