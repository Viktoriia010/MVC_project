using Microsoft.EntityFrameworkCore;
namespace MvcProject.Models;

public class ProductContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public ProductContext(DbContextOptions<ProductContext> options)
       : base(options)
    {
        if (Database.EnsureCreated())
        {
            Products?.Add(new Product { name = "Milk", price=2, stock_qty=4, category_id = 3 });
            Products?.Add(new Product { name = "Butter", price = 6, stock_qty = 4, category_id = 3 });
            Products?.Add(new Product {name = "Pizza", price = 9, stock_qty = 8, category_id = 3 });
            SaveChanges();
        }
    }
}