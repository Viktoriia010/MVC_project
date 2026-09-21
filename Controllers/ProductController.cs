using Microsoft.AspNetCore.Mvc;
using MvcProject.Models;

namespace MvcProject.Controllers;

public class ProductController(IProductService _service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var products = await _service.GetAllProductsAsync(cancellationToken);

        return View(products);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product,CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        await _service.CreateProductAsync(product, cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id,CancellationToken cancellationToken)
    {
        var product = await _service.GetProductByIdAsync(id, cancellationToken);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var product = await _service.GetProductByIdAsync(id,cancellationToken);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> DeletePost(int id,CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteProductById(id, cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id,Product product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        var updatedProduct = await _service.UpdateProductById(product,id,cancellationToken);

        if (updatedProduct == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id,CancellationToken cancellationToken)
    {
        var product = await _service.GetProductByIdAsync(id, cancellationToken);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}