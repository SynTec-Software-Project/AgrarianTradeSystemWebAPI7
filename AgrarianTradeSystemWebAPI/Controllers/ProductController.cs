using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductServices _productServices;

		public ProductController(IProductServices productServices)
		{
			_productServices = productServices;
		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetAllProduct()
		{

			return await _productServices.GetAllProduct();
		}

		//get data by id
		[HttpGet("{id}")]
		public async Task<ActionResult<List<Product>>> GetSingleProduct(int id)
		{
			var result = await _productServices.GetSingleProduct(id);
			if (result is null)
				return NotFound("product is not found");
			return Ok(result);
		}

		//post data
		[HttpPost]
		public async Task<ActionResult<List<Product>>> AddProduct(Product product)
		{
			var result = await _productServices.AddProduct(product);
			return Ok(result);


		}

		//update product
		[HttpPut("{id}")]
		public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product request)
		{

			var result = await _productServices.UpdateProduct(id, request);
			if (result is null)
				return NotFound("product is not found");

			return Ok(result);
		}

		//delete products
		public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
		{

			var result = await _productServices.DeleteProduct(id);
			if (result is null)
				return NotFound("product is not found");
			return Ok(result);
		}




	}
}

