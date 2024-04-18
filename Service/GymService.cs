using Contracts;
using Entities;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace Service
{
    internal sealed class GymService : IGymService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public GymService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task CreateGymAsync(GymForCreationDto gymDto)
        {
            var gymToCreate = new Gym
            {
                Id = Guid.NewGuid(),
                Name = gymDto.Name,
                Address = gymDto.Address,
                OpeningTime = TimeSpan.Parse(gymDto.OpeningTime!),
                ClosingTime = TimeSpan.Parse(gymDto.ClosingTime!),
            };

            _repository.Gym.CreateGym(gymToCreate);
            await _repository.SaveAsync();
        }

        public async Task DeleteGymAsync(Guid gymId)
        {
            var gymEntity = await _repository.Gym.GetGymAsync(gymId, trackChanges: true);
            if (gymEntity is null)
                throw new GymNotFoundException($"Gym by id {gymId} does not exist in the database.");

            _repository.Gym.DeleteGym(gymEntity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<GymForReturdDto>> GetAllGymsAsync()
        {
            var gymEntities = await _repository.Gym.GetAllGymsAsync(trackChanges: false);

            List<GymForReturdDto> res = new List<GymForReturdDto>();
            foreach (var gymEntity in gymEntities)
            {
                var toAppend = new GymForReturdDto
                {
                    Id = gymEntity.Id,
                    Name = gymEntity.Name,
                    Address = gymEntity.Address,
                    OpeningTime = gymEntity.OpeningTime,
                    ClosingTime = gymEntity.ClosingTime,
                };
                res.Add(toAppend);
            }

            return res;
        }

        public async Task<GymForReturdDto?> GetGymAsync(Guid gymId)
        {
            var gymEntity = await _repository.Gym.GetGymAsync(gymId, trackChanges: false);
            if (gymEntity is null)
                throw new GymNotFoundException($"Gym by id {gymId} does not exist in the database.");

            var res = new GymForReturdDto
            {
                Id = gymEntity.Id,
                Name = gymEntity.Name,
                Address = gymEntity.Address,
                OpeningTime = gymEntity.OpeningTime,
                ClosingTime = gymEntity.ClosingTime,
            };

            return res;
        }

        public async Task UpdateGymAsync(Guid gymId, GymForUpdateDto gymDto)
        {
            var gymEntity = await _repository.Gym.GetGymAsync(gymId, trackChanges: true);
            if (gymEntity is null)
                throw new GymNotFoundException($"Gym by id {gymId} does not exist in the database.");

            gymEntity.Name = gymDto.Name;
            gymEntity.Address = gymDto.Address;
            gymEntity.OpeningTime = TimeSpan.Parse(gymDto.OpeningTime!);
            gymEntity.ClosingTime = TimeSpan.Parse(gymDto.ClosingTime!);

            _repository.Gym.UpdateGym(gymEntity);
            await _repository.SaveAsync();
        }
    }
}
