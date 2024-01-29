//using AgrarianTradeSystemWebAPI.Models;
//using AgrarianTradeSystemWebAPI.Services.ProductServices;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace AgrarianTradeSystemWebAPI.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class FileController : ControllerBase
//	{
//		private readonly IFileServices _fileService;

//		public FileController(IFileServices fileService)
//		{
//			_fileService = fileService;
//		}

//		[HttpPost]
//		[Route("upload")]
//		public async Task<IActionResult> Upload([FromForm] FileModel fileModel)
//		{
//			try
//			{
//				string newFileName = await _fileService.Upload(fileModel);

//				// Return the generated new filename
//				return Ok(newFileName);
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading image: {ex.Message}");
//			}
//		}

//		[HttpGet]
//		[Route("get")]
//		public async Task<IActionResult> Get(String name)
//		{
//			var imageFileStream = await _fileService.Get(name);
//			string fileType = "jpeg";

//			if (name.Contains("png"))
//			{
//				fileType = "png";
//			}

//			return File(imageFileStream, $"image/{fileType}");
//		}

//		[HttpGet]
//		[Route("download")]
//		public async Task<IActionResult> Download(String name)
//		{
//			var imageFileStream = await _fileService.Get(name);
//			string fileType = "jpeg";

//			if (name.Contains("png"))
//			{
//				fileType = "png";
//			}

//			return File(imageFileStream, $"image/{fileType}", $"blobfile.{fileType}");
//		}
//	}
//}
