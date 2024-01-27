using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgrarianTradeSystemWebAPI.Services.ProductServices
{
	public class ProductServices : IProductServices
	{
		private readonly DataContext _context;

		public ProductServices(DataContext context)
		{
			_context = context;

		}

		public DataContext Context { get; }

		public async Task<List<Product>> GetAllProduct()
		{

			var product = await _context.Products.ToListAsync(); //retrieve data from db
			return product;
		}

		//get single data by id
		public async Task<Product?> GetSingleProduct(int id)
		{
			var hero = await _context.Products.FindAsync(id);  //find data from db
			if (hero == null)
				return null;
			return hero;
		}

		public async Task<List<Product>> AddProduct(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return await _context.Products.ToListAsync();
		}

		//update
		public async Task<List<Product>?> UpdateProduct(int id, Product request)
		{
			var product = await _context.Products.FindAsync(id); //find data from db
			if (product == null)
				return null;
			product.ProductTitle = request.ProductTitle;
			product.ProductDescription = request.ProductDescription;
			product.UnitPrice = request.UnitPrice;
			product.ProductImage = request.ProductImage;
			product.ProductType = request.ProductType;
			product.Category = request.Category;
			product.AvailableStock = request.AvailableStock;
			product.MinimumQuantity = request.MinimumQuantity;
			product.DateCreated = request.DateCreated;
			await _context.SaveChangesAsync();
			return await _context.Products.ToListAsync();
		}

		//delete
		public async Task<List<Product>?> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
				return null;

			_context.Products.Remove(product);

			await _context.SaveChangesAsync();
			return await _context.Products.ToListAsync();
		}

	}
}
