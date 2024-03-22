using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgrarianTradeSystemWebAPI.Services.ProductServices
{
	public class ProductServices : IProductServices
	{
		private readonly DataContext _context;
		private const string AzureContainerName = "products";
		private readonly IFileServices _fileServices;
		public ProductServices(DataContext context, IFileServices fileServices)
		{
			_context = context;
			_fileServices = fileServices;

		}

		public DataContext Context { get; }

		//get all products
		public async Task<List<Product>> GetAllProduct()
		{

			var product = await _context.Products.ToListAsync(); //retrieve data from db
			return product;
		}

		public async Task<List<Product>> GetAllProductsSortedByPriceAsync(bool ascending = true)
		{
			if (ascending)
			{
				return await _context.Products.OrderBy(p => p.UnitPrice).ToListAsync();
			}
			else
			{
				return await _context.Products.OrderByDescending(p => p.UnitPrice).ToListAsync();
			}
		}

		//get single data by id
		public async Task<Product?> GetSingleProduct(int id)
		{
			var product = await _context.Products.FindAsync(id); 
			if (product == null)
				return null;
			return product;
		}

		//add products
		public async Task<List<Product>> AddProduct(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return await _context.Products.ToListAsync();
		}

		//update
		public async Task<List<Product>?> UpdateProduct(int id, [FromForm] Product request, String newFileUrl)
		{
			//find data from db
			var product = await _context.Products.FindAsync(id);
			if (product == null)
				return null;
			//get product image url Name
			var fileName = product.ProductImageUrl;

			//delete image from azure storage
			await _fileServices.Delete(fileName, AzureContainerName);

			//update database
			product.ProductTitle = request.ProductTitle;
			product.ProductDescription = request.ProductDescription;
			product.ProductImageUrl = newFileUrl;
			product.UnitPrice = request.UnitPrice;
			product.ProductType = request.ProductType;
			product.Category = request.Category;
			product.AvailableStock = request.AvailableStock;
			product.MinimumQuantity = request.MinimumQuantity;
			await _context.SaveChangesAsync();
			return await _context.Products.ToListAsync();
		}

		//delete
		public async Task<List<Product>?> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
				return null;

			//get product image url Name
			var fileName = product.ProductImageUrl;

			//delete image from azure storage
			await _fileServices.Delete(fileName, AzureContainerName);

			//remove product from database
			_context.Products.Remove(product);

			//save changes
			await _context.SaveChangesAsync();

			return await _context.Products.ToListAsync();
		}

	}
}
