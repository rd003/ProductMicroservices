using System.ComponentModel.DataAnnotations;

namespace CategoryService.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = String.Empty;

    public Category()
    {
    }

    public Category(string name)
    {
        Name = name;
    }
}
