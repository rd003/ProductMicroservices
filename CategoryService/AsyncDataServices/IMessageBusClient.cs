using CategoryService.DTOS;
using CategoryService.Models;

namespace CategoryService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishNewCategory(CategoryPublishedDTO category);
}
