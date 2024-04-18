using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ISubscriptionTypeService
    {
        Task CreateSubscriptionType(SubscriptionTypeForCreationDto subscriptionTypeForCreationDto);
        Task<IEnumerable<SubscriptionTypeForReturnDto>> GetAllSubscriptionTypesAsync();
        Task<SubscriptionTypeForReturnDto?> GetSubscriptionTypeAsync(Guid subscriptionTypeId);
        Task<IEnumerable<SubscriptionTypeForReturnDto>> GetAllSubscriptionTypesForGymAsync(Guid gymId);
        Task DeleteSubscriptionType(Guid subscriptionTypeId);
        Task UpdateSubscriptionType(Guid subscriptionTypeId, SubscriptionTypeForUpdateDto subscriptionTypeForUpdateDto);
    }
}
