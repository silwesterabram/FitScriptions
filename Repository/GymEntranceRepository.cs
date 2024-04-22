using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GymEntranceRepository : RepositoryBase<GymEntrance>, IGymEntranceRepository
    {
        public GymEntranceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateEntrance(GymEntrance gymEntrance) => Create(gymEntrance);

        public async Task<List<GymEntrance>> GetEnrtancesByUserId(Guid userId, bool trackChanges) =>
            await FindByCondition(ge => ge.UserId!.Equals(userId.ToString()), trackChanges)
                .ToListAsync();

        public Task<GymEntrance?> GetGymEntranceByIdAsync(Guid gymEntranceId, bool trackChanges) =>
            FindByCondition(ge => ge.Id.Equals(gymEntranceId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
