using Entities;

namespace Contracts
{
    public interface IUserSubscriptionRepository
    {
        void DeleteUserSubscriptionAsync(UserSubscription userSubscription);
        void UpdateUserSubscriptionAsync(UserSubscription userSubscription);
        void CreateUserSubscriptionAsync(UserSubscription userSubscription);
        Task<IEnumerable<UserSubscription>> GetAllUserSubscriptionsAsync(bool trackChanges);
        Task<UserSubscription?> GetUserSubscriptionAsync(Guid userSubscriptionId, bool trackChanges);
        Task<List<UserSubscription>> GetAllUserSubscriptionsByUserId(Guid userId, bool trackChanges);
    }
}
