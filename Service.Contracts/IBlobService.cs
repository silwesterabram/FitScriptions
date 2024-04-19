using Microsoft.AspNetCore.Http;

namespace Service.Contracts
{
    public interface IBlobService
    {
        public Task<List<string>> GetAllBlobNamesAsync(string containerName);
        public Task<string> UploadFileAsync(IFormFile file, string containerName);
        public Task<string> UploadFileAsync(byte[] file, string containerName, string fileName);
        public Task DeleteFileAsync(string fileName, string containerName);
    }
}
