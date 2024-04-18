using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace FitScriptions.Presentation.Controllers
{
    [ApiController]
    [Route("api/subscriptiontype")]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly IServiceManager _service;

        public SubscriptionTypeController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriptionType([FromBody] SubscriptionTypeForCreationDto subscriptionTypeForCreationDto)
        {
            if (subscriptionTypeForCreationDto is null)
                throw new BadRequestException("Subscriptiontype creation document is null");

            await _service.SubscriptionTypeService.CreateSubscriptionType(subscriptionTypeForCreationDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriptionTypes()
        {
            var result = await _service.SubscriptionTypeService.GetAllSubscriptionTypesAsync();
            return Ok(result);
        }

        [HttpGet("{subscriptionTypeId:guid}")]
        public async Task<IActionResult> GetSubscriptionType(Guid subscriptionTypeId)
        {
            var result = await _service.SubscriptionTypeService.GetSubscriptionTypeAsync(subscriptionTypeId);
            return Ok(result);
        }

        [HttpGet("gym/{gymId:guid}")]
        public async Task<IActionResult> GetSubscriptionTypesForGym(Guid gymId)
        {
            var result = await _service.SubscriptionTypeService.GetAllSubscriptionTypesForGymAsync(gymId);
            return Ok(result);
        }

        [HttpPut("{subscriptionTypeId:guid}")]
        public async Task<IActionResult> UpdateSubscriptionType(Guid subscriptionTypeId, [FromBody] SubscriptionTypeForUpdateDto subscriptionTypeForUpdateDto)
        {
            if (subscriptionTypeForUpdateDto is null)
                throw new BadRequestException("Subscriptiontype update document is null");

            await _service.SubscriptionTypeService.UpdateSubscriptionType(subscriptionTypeId, subscriptionTypeForUpdateDto);
            return Ok();
        }

        [HttpDelete("{subscriptionTypeId:guid}")]
        public async Task<IActionResult> DeleteSubscriptionType(Guid subscriptionTypeId)
        {
            await _service.SubscriptionTypeService.DeleteSubscriptionType(subscriptionTypeId);
            return NoContent();
        }
    }
}
