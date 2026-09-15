using Microsoft.AspNetCore.Mvc;
using MvcProject.Models;

namespace MvcProject.Controllers;

public class ProductController:Controller
{
    ProductContext db;

    public ProductController(ProductContext context)
    {
        db = context;
    }

    public async Task<IActionResult> Index()
    {

        IEnumerable<Product> products = await Task.Run(() => db.Products);

        ViewBag.Products = products;

        return View();
    }
}

