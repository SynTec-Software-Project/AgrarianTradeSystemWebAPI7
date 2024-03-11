using AgrarianTradeSystemWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Services.ProductServices
{
	public interface IProductServices
	{
		Task<List<Product>> GetAllProduct();
		Task<Product?> GetSingleProduct(int id);

		Task<List<Product>> AddProduct(Product product);

		Task<List<Product>?> UpdateProduct(int id, [FromForm] Product request, String newFileUrl);

		Task<List<Product>?> DeleteProduct(int id);

		Task<List<Product>> GetAllProductsSortedByPriceAsync(bool ascending = true);

	}
}
