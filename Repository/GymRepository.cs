using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GymRepository : RepositoryBase<Gym>, IGymRepository
    {
        public GymRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateGym(Gym gym) => Create(gym);

        public void DeleteGym(Gym gym) => Delete(gym);

        public async Task<IEnumerable<Gym>> GetAllGymsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(g => g.Name)
                .ToListAsync();

        public async Task<Gym?> GetGymAsync(Guid gymId, bool trackChanges) => 
            await FindByCondition(g => g.Id.Equals(gymId), trackChanges)
                .SingleOrDefaultAsync();

        public void UpdateGym(Gym gym) => Update(gym);
    }
}
