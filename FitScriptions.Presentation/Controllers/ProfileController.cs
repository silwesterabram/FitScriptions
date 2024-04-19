using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace FitScriptions.Presentation.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly BlobService _blobService;

        public ProfileController(IServiceManager service, IConfiguration configuration)
        {
            _service = service;
            var connectionString = configuration.GetConnectionString("AzureBlobStorageConnection");
            _blobService = new BlobService(connectionString!);
        }
        [HttpGet]
        public IActionResult GetAllProfiles()
        {
            var profiles = _service.ProfileService.GetAllProfiles();
            return Ok(profiles);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var profile = await _service.ProfileService.GetProfileAsync(userId);
            return Ok(profile);
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> UpdateProfile(Guid userId, [FromBody] ProfileForUpdateDto profileForUpdateDto)
        {
            await _service.ProfileService.UpdateProfile(userId, profileForUpdateDto);
            return NoContent();
        }


        [HttpPut("{userId:guid}/update-picture")]
        public async Task<IActionResult> UpdateProfilePicture(Guid userId, [FromForm] IFormFile file)
        {
            var currentProfileUri = await _service.ProfileService.GetProfilePictureUriAsync(userId);

            if (currentProfileUri != "")
            {
                string fileName = Shared.Helpers.ImageUrlLogic.ExtractFileNameFromProfilePictureUri(currentProfileUri!);
                await _blobService.DeleteFileAsync(fileName, userId.ToString());
            }

            var newProfileUri = await _blobService.UploadFileAsync(file, userId.ToString());
            await _service.ProfileService.SaveProfilePictureUri(userId, newProfileUri);

            return Ok(newProfileUri);
        }

        [HttpDelete("{userId:guid}/delete-picture")]
        public async Task<IActionResult> DeleteProfilePicture(Guid userId)
        {
            var currentProfilePictureUrl = await _service.ProfileService.GetProfilePictureUriAsync(userId);
            if (currentProfilePictureUrl == "")
                throw new ProfilePictureNotFoundException($"There is no active profile picture at user with id {userId}");

            string fileName = Shared.Helpers.ImageUrlLogic.ExtractFileNameFromProfilePictureUri(currentProfilePictureUrl!);

            await _blobService.DeleteFileAsync(fileName, userId.ToString());
            await _service.ProfileService.SaveProfilePictureUri(userId, "");
            return NoContent();
        }

        [HttpPut("{userId:guid}/update-qr")]
        public async Task<IActionResult> UpdateProfileQRCode(Guid userId)
        {
            var currentQrCodeUri = await _service.ProfileService.GetQRCodeUriAsync(userId);
            if (currentQrCodeUri != "")
            {
                string fileName = Shared.Helpers.ImageUrlLogic.ExtractFileNameFromProfilePictureUri(currentQrCodeUri!);
                await _blobService.DeleteFileAsync(fileName, userId.ToString());
            }

            var newQrCode = await _service.ProfileService.GenerateQRCode(userId);
            var newQrCodeUri = await _blobService.UploadFileAsync(newQrCode, userId.ToString(), Guid.NewGuid().ToString() + $".png");

            await _service.ProfileService.SaveProfileQRCodeUri(userId, newQrCodeUri);
            return Ok(newQrCodeUri);
        }

        [HttpDelete("{userId:guid}/delete-qr")]
        public async Task<IActionResult> DeleteProfileQRCode(Guid userId)
        {
            var currentQrCodeUri = await _service.ProfileService.GetQRCodeUriAsync(userId);
            if (currentQrCodeUri == "")
                throw new QRCodeNotFoundException($"There is no active QR code at user with id {userId}");

            string fileName = Shared.Helpers.ImageUrlLogic.ExtractFileNameFromProfilePictureUri(currentQrCodeUri!);

            await _blobService.DeleteFileAsync(fileName, userId.ToString());
            await _service.ProfileService.SaveProfileQRCodeUri(userId, "");
            return NoContent();
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteProfile(Guid userId)
        {
            await _service.ProfileService.DeleteProfile(userId);
            return NoContent();
        }
    }
}
