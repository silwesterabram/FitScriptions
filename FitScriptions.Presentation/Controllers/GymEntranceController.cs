using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace FitScriptions.Presentation.Controllers
{
    [ApiController]
    [Route("api/entrance")]
    public class GymEntranceController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly BlobService _blobService;

        public GymEntranceController(IServiceManager service, IConfiguration configuration)
        {
            _service = service;
            var connectionString = configuration.GetConnectionString("AzureBlobStorageConnection");
            _blobService = new BlobService(connectionString!);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetEntrancesByUserId(Guid userId)
        {
            var result = await _service.GymEntranceService.GetGymEntrancesByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("{entranceId:guid}")]
        public async Task<IActionResult> GetEntrance(Guid entranceId)
        {
            var result = await _service.GymEntranceService.GetGymEntranceByIdAsync(entranceId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntrance([FromBody] GymEntraceForCreationDto gymEntranceForCreationDto)
        {
            if (gymEntranceForCreationDto is null)
                throw new BadRequestException("GymEntraceForCreationDto object is null");

            await _service.GymEntranceService.CreateGymEntrance(gymEntranceForCreationDto);
            return StatusCode(201);
        }
    }
}
