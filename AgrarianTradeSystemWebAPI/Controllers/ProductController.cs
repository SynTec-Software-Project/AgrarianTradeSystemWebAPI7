using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Services.ProductServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductServices _productServices;
		private readonly IMapper _mapper;
		private readonly IFileServices _fileServices;
		private const string AzureContainerName = "products";

		public ProductController(IProductServices productServices, IMapper mapper, IFileServices fileServices)
		{
			_productServices = productServices;
			_mapper = mapper;
			_fileServices = fileServices;
		}
		//get all products
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

		//post products
		[HttpPost]
		public async Task<ActionResult<List<Product>>> AddProduct([FromForm] ProductDto productDto, IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("No file uploaded.");
			}

			// upload file to the azure storage and get link
			var fileUrl = await _fileServices.Upload(file, AzureContainerName);

			// Map the DTO to the Product entity
			var product = _mapper.Map<Product>(productDto);

			//set the imageUrl from azure blob storage
			product.ProductImageUrl = fileUrl;

			// Add the product to the database
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
		[HttpDelete("{id}")]
		public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
		{

			var result = await _productServices.DeleteProduct(id);
			if (result is null)
				return NotFound("product is not found");
			return Ok(result);
		}

	}
}

