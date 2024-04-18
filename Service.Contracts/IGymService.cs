using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IGymService
    {
        Task<IEnumerable<GymForReturdDto>> GetAllGymsAsync();
        Task<GymForReturdDto?> GetGymAsync(Guid gymId);
        Task DeleteGymAsync(Guid gymId);
        Task CreateGymAsync(GymForCreationDto gymDto);
        Task UpdateGymAsync(Guid gymId, GymForUpdateDto gymDto);
    }
}
