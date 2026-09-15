using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces.Services;
using Shop.Application.Services;

namespace MvcProject.Controllers;

public class CategoryController(ICategoryService _categoryService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {

        var categories = await _categoryService.GetAllCategoriesAsync(cancellationToken);
        ViewBag.Categories = categories;

        return View();
    }
}
