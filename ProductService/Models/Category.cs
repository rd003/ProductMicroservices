namespace ProductService.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // referece of CategoryService's Category(Id)
    public int ExternalId { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}
