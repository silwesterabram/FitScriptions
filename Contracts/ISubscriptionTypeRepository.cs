using Entities;

namespace Contracts
{
    public interface ISubscriptionTypeRepository
    {
        void CreateSubscriptionType(SubscriptionType subscriptionType);
        void DeleteSubscriptionType(SubscriptionType subscriptionType);
        void UpdateSubscriptionType(SubscriptionType subscriptionType);
        Task<IEnumerable<SubscriptionType>> GetAllSubscriptionTypesAsync(bool trackChanges);
        Task<SubscriptionType?> GetSubscriptionTypeAsync(Guid subscriptionTypeId, bool trackChanges);
        Task<IEnumerable<SubscriptionType>> GetAllSubscriptionTypesForGymAsync(Guid gymId, bool trackChanges);
    }
}
