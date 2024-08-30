

using System.Text.Json;
using ProductService.Data;
using ProductService.DTOS;
using ProductService.Extensions;

namespace ProductService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    public EventProcessor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public void ProcessEvent(string message)
    {
        EventType eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.CategoryPublished:
                AddCategory(message);
                break;
            default:
                break;
        }
    }

    private async void AddCategory(string message)
    {
        try
        {
            CategoryPublishedDTO? categoryPublishedDto = DeserializeCategoryPublishedMessage(message);
            if (categoryPublishedDto != null)
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetService<ProductContext>();
                if (context == null) { return; }
                var category = categoryPublishedDto.ToCategory();
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not deserialize the message: {ex.Message}");
        }
    }

    private EventType DetermineEvent(string message)
    {
        Console.WriteLine("===> Determining Event");

        var deserializedMessage = DeserializeCategoryPublishedMessage(message);
        switch (deserializedMessage?.Event)
        {
            case "Category_Published":
                Console.WriteLine("===> Category Published Event Detected");
                return EventType.CategoryPublished;
            default:
                Console.WriteLine("===> Could not determine the event type");
                return EventType.Undetermined;
        }
    }

    private CategoryPublishedDTO? DeserializeCategoryPublishedMessage(string message)
    {
        return JsonSerializer.Deserialize<CategoryPublishedDTO>(message);
    }
}

enum EventType
{
    CategoryPublished,
    Undetermined
}
