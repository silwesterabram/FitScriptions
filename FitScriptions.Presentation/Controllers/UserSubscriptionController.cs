using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace FitScriptions.Presentation.Controllers
{
    [ApiController]
    [Route("api/usersubscription")]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly BlobService _blobService;

        public UserSubscriptionController(IServiceManager service, IConfiguration configuration)
        {
            _service = service;
            var connectionString = configuration.GetConnectionString("AzureBlobStorageConnection");
            _blobService = new BlobService(connectionString!);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserSubscriptions()
        {
            var result = await _service.UserSubscriptionService.GetUserSubscriptionsAsync();
            return Ok(result);
        }

        [HttpGet("{userSubscriptionId:guid}")]
        public async Task<IActionResult> GetUserSubscription(Guid userSubscriptionId)
        {
            var result = await _service.UserSubscriptionService.GetUserSubscriptionByIdAsync(userSubscriptionId);
            return Ok(result);
        }

        [HttpGet("userid/{userId:guid}")]
        public async Task<IActionResult> GetAllUserSubscriptionsByUserId(Guid userId)
        {
            var result = await _service.UserSubscriptionService.GetUserSubscriptionsByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserSubscription([FromBody] UserSubscriptionForCreationDto userSubscriptionForCreationDto)
        {
            if (userSubscriptionForCreationDto is null)
                throw new BadRequestException("UserSubscriptionForCreationDto object is null");

            await _service.UserSubscriptionService.CreateUserSubscription(userSubscriptionForCreationDto);
            return StatusCode(201);
        }

        [HttpPut("{userSubscriptionId:guid}/update-qr")]
        public async Task<IActionResult> UpdateQRCode(Guid userSubscriptionId)
        {
            var currentQRCodeUri = await _service.UserSubscriptionService.GetQRCodeUrlAsync(userSubscriptionId);
            if (currentQRCodeUri != "")
            {
                string fileName = Shared.Helpers.ImageUrlLogic.ExtractFileNameFromImageUri(currentQRCodeUri!);
                await _blobService.DeleteFileAsync(fileName, userSubscriptionId.ToString());
            }

            var newQrCode = await _service.UserSubscriptionService.GenerateQRCode(userSubscriptionId);
            var newQrCodeUrl = await _blobService.UploadFileAsync(newQrCode, userSubscriptionId.ToString(), Guid.NewGuid().ToString() + ".png");

            await _service.UserSubscriptionService.SaveQRCodeUrl(userSubscriptionId, newQrCodeUrl);
            return Ok(newQrCodeUrl);
        }

        [HttpDelete("{userSubscriptionId:guid}")]
        public async Task<IActionResult> DeleteUserSubscription(Guid userSubscriptionId)
        {
            await _service.UserSubscriptionService.DeleteUserSubscription(userSubscriptionId);
            return NoContent();
        }
    }
}
