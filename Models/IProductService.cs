
namespace MvcProject.Models;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);

    Task<int> CreateProductAsync( Product product,CancellationToken cancellationToken);

    Task<Product?> GetProductByIdAsync( int id, CancellationToken cancellationToken);

    Task<Product?> UpdateProductById(Product updatedProduct,int id,CancellationToken cancellationToken);

    Task<bool> DeleteProductById(int id,CancellationToken cancellationToken);
}