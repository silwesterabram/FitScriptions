using Entities;

namespace Contracts
{
    public interface IGymRepository
    {
        Task<IEnumerable<Gym>> GetAllGymsAsync(bool trackChanges);
        Task<Gym?> GetGymAsync(Guid gymId, bool trackChanges);
        void DeleteGym(Gym gym);
        void CreateGym(Gym gym);
        void UpdateGym(Gym gym);
    }
}
