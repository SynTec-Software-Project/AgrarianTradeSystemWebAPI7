using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Services.ProductServices
{
	public interface IProductServices
	{
		Task<List<Product>> GetAllProduct();
		Task<Product?> GetSingleProduct(int id);

		Task<List<Product>> AddProduct(IFormFile file, Product product);

		Task<List<Product>?> UpdateProduct(int id, Product request);

		Task<List<Product>?> DeleteProduct(int id);

	}
}
