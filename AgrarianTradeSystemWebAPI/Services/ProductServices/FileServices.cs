using AgrarianTradeSystemWebAPI.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
namespace AgrarianTradeSystemWebAPI.Services.ProductServices
{
	public class FileServices : IFileServices
	{
		private string _containerName;
		private readonly BlobServiceClient _blobServiceClient;
	

		public FileServices(BlobServiceClient blobServiceClient)
		{
			_blobServiceClient = blobServiceClient;
			
		}

		public async Task<string> Upload(IFormFile file, string containerName)
		{
			_containerName = containerName;
			// Generate a UUID
			string uuid = Guid.NewGuid().ToString();

			// Construct new filename with UUID
			string newFileName = $"{uuid}";

			// Create container instance
			var containerInstance = _blobServiceClient.GetBlobContainerClient(_containerName);

			// Create blob instance with the new filename and provide the file extension
			string fileExtension = Path.GetExtension(file.FileName);
			string blobName = $"{newFileName}{fileExtension}";
			var blobInstance = containerInstance.GetBlobClient(blobName);

			// File save in storage
			using (Stream stream = file.OpenReadStream())
			{
				await blobInstance.UploadAsync(stream);
			}

			// Return the generated new filename
			return blobName;
		}




		public async Task<Stream> Get(String name)
		{
			//create container instance
			var containerInstance = _blobServiceClient.GetBlobContainerClient("products");

			//create blob instance
			var blobInstance = containerInstance.GetBlobClient(name);

			var downloadContent = await blobInstance.DownloadAsync();

			return downloadContent.Value.Content;

		}
	}
}
