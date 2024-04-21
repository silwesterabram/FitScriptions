using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserSubscriptionRepository : RepositoryBase<UserSubscription>, IUserSubscriptionRepository
    {
        public UserSubscriptionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUserSubscriptionAsync(UserSubscription userSubscription) => Create(userSubscription);

        public void DeleteUserSubscriptionAsync(UserSubscription userSubscription) => Delete(userSubscription);

        public async Task<IEnumerable<UserSubscription>> GetAllUserSubscriptionsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(us => us.IsActive)
                .ToListAsync();

        public async Task<List<UserSubscription>> GetAllUserSubscriptionsByUserId(Guid userId, bool trackChanges) =>
            await FindByCondition(us => us.UserId!.Equals(userId.ToString()), trackChanges)
                .OrderBy(us => us.IsActive)
                .ToListAsync();

        public async Task<UserSubscription?> GetUserSubscriptionAsync(Guid userSubscriptionId, bool trackChanges) =>
            await FindByCondition(us => us.Id.Equals(userSubscriptionId), trackChanges)
                .SingleOrDefaultAsync();

        public void UpdateUserSubscriptionAsync(UserSubscription userSubscription) => Update(userSubscription);
    }
}
