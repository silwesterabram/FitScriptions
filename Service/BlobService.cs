using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Shared.Exceptions;

namespace Service
{
    public class BlobService : IBlobService
    {
        private readonly string _connectionString;
        private readonly BlobServiceClient _blobClient;

        public BlobService(string connectionString)
        {
            _connectionString = connectionString;
            _blobClient = new BlobServiceClient(_connectionString);
        }

        public async Task DeleteFileAsync(string fileName, string containerName)
        {
            var container = _blobClient.GetBlobContainerClient(containerName);

            if (!container.GetBlobClient(fileName).Exists())
                throw new BlobNameNotFoundException($"Blob with name {fileName} not found in container {containerName}");

            await container.DeleteBlobAsync(fileName);
        }

        public async Task<List<string>> GetAllBlobNamesAsync(string containerName)
        {
            var container = _blobClient.GetBlobContainerClient(containerName);

            if (container is null)
                throw new BlobNameNotFoundException($"Container with name {containerName} not found");

            List<string> items = new List<string>();

            await foreach (var blobItem in container.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            var container = _blobClient.GetBlobContainerClient(containerName);

            if (!await container.ExistsAsync())
                await container.CreateAsync(PublicAccessType.Blob);

            if (file is null || file.Length == 0)
                throw new BlobFileNullException("File is null or empty");

            if (!file.ContentType.StartsWith("image/"))
                throw new BlobFileFormatException("File is not an image");

            if (file.Length > Shared.Constants.MaxFileSize)
                throw new BlobFileSizeException("File is too large");

            var blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var blobClient = _blobClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }
    }
}
