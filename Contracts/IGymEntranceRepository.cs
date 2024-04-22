using Entities;

namespace Contracts
{
    public interface IGymEntranceRepository
    {
        Task<GymEntrance?> GetGymEntranceByIdAsync(Guid gymEntranceId, bool trackChanges);
        Task<List<GymEntrance>> GetEnrtancesByUserId(Guid userId, bool trackChanges);
        void CreateEntrance(GymEntrance gymEntrance);
    }
}
