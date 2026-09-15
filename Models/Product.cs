using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProject.Models;

public class Product
{
    public int id { get; set; }

    public string name { get; set; } = string.Empty;
    public string? description { get; set; }

    public decimal price { get; set; }
    public int stock_qty { get; set; } = 0;
    public bool is_active { get; set; } = true;

    public int category_id { get; set; }

   // public ICollection<string> images { get; set; } = new List<string>();
}
