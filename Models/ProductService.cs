using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs.ProductDTOs;
using ShopDomain.Models;

namespace MvcProject.Models;

public class ProductService(ProductContext _context): IProductService
{

    public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CreateProductAsync(Product product,CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return product.id;
    }

    public async Task<Product?> GetProductByIdAsync(int id,CancellationToken cancellationToken)
    {
        return await _context.Products
            .FirstOrDefaultAsync(m => m.id == id, cancellationToken);
    }

    public async Task<Product?> UpdateProductById(Product updatedProduct,int id,CancellationToken cancellationToken)
    {
        var product = await GetProductByIdAsync(id, cancellationToken);

        if (product == null)
        {
            return null;
        }

        product.name = updatedProduct.name;
        product.description = updatedProduct.description;
        product.price = updatedProduct.price;
        product.stock_qty = updatedProduct.stock_qty;
        product.is_active = updatedProduct.is_active;
        product.category_id = updatedProduct.category_id;

        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<bool> DeleteProductById(int id,CancellationToken cancellationToken)
    {
        var product = await GetProductByIdAsync(id, cancellationToken);

        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}