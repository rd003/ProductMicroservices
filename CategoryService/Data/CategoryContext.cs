
using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Data;

public class CategoryContext : DbContext
{
    public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }
}
