using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Shared.Helpers
{
    public static class QRCodeConverter
    {
        public static IFormFile ConvertByteArrayToIFormFile(byte[] file, string fileName)
        {
            // Create a memory stream from the byte array
            using var memoryStream = new MemoryStream(file);

            // Create an IFormFile instance
            var formFile = new FormFile(memoryStream, 0, file.Length, fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };

            return formFile;
        }
    }
}
