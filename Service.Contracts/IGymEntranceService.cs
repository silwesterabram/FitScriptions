using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IGymEntranceService
    {
        Task<GymEntranceForReturnDto> GetGymEntranceByIdAsync(Guid gymEntranceId);
        Task<List<GymEntranceForReturnDto>> GetGymEntrancesByUserIdAsync(Guid userId);
        Task CreateGymEntrance(GymEntraceForCreationDto gymEntranceForCreationDto);
    }
}
