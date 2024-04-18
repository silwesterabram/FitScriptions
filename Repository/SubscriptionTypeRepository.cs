using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SubscriptionTypeRepository : RepositoryBase<SubscriptionType>, ISubscriptionTypeRepository
    {
        public SubscriptionTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateSubscriptionType(SubscriptionType subscriptionType) => Create(subscriptionType);

        public void DeleteSubscriptionType(SubscriptionType subscriptionType) => Delete(subscriptionType);

        public async Task<IEnumerable<SubscriptionType>> GetAllSubscriptionTypesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(st => st.Name)
                .ToListAsync();

        public async Task<IEnumerable<SubscriptionType>> GetAllSubscriptionTypesForGymAsync(Guid gymId, bool trackChanges) =>
            await FindByCondition(st => st.GymId.Equals(gymId), trackChanges)
                .OrderBy(st => st.Name)
                .ToListAsync();

        public async Task<SubscriptionType?> GetSubscriptionTypeAsync(Guid subscriptionTypeId, bool trackChanges) =>
            await FindByCondition(st => st.Id.Equals(subscriptionTypeId), trackChanges)
                .SingleOrDefaultAsync();

        public void UpdateSubscriptionType(SubscriptionType subscriptionType) => Update(subscriptionType);
    }
}
