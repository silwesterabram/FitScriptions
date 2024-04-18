using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;
using Shared.Helpers;

namespace FitScriptions.Presentation.Controllers
{
    [Route("api/gym")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GymController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGyms()
        {
            var result = await _service.GymService.GetAllGymsAsync();

            return Ok(result);
        }

        [HttpGet("{gymId:guid}")]
        public async Task<IActionResult> GetGym(Guid gymId)
        {
            var result = await _service.GymService.GetGymAsync(gymId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGym([FromBody] GymForCreationDto gymDto)
        {
            if (gymDto is null)
                throw new BadRequestException("GymForCreationDto object is null");

            GymDtoChecker.IsValidGymDto(gymDto);

            await _service.GymService.CreateGymAsync(gymDto);
            return StatusCode(201);
        }

        [HttpPut("{gymId:guid}")]
        public async Task<IActionResult> UpdateGym(Guid gymId, [FromBody] GymForUpdateDto gymDto)
        {
            if (gymDto is null)
                throw new BadRequestException("GymForUpdateDto object is null");

            GymDtoChecker.IsValidGymDto(gymDto);

            await _service.GymService.UpdateGymAsync(gymId, gymDto);
            return Ok();
        }

        [HttpDelete("{gymId:guid}")]
        public async Task<IActionResult> DeleteGym(Guid gymId)
        {
            await _service.GymService.DeleteGymAsync(gymId);
            return NoContent();
        }
    }
}
