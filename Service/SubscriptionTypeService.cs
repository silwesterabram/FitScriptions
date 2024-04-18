using Contracts;
using Entities;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace Service
{
    internal sealed class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public SubscriptionTypeService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task CreateSubscriptionType(SubscriptionTypeForCreationDto subscriptionTypeForCreationDto)
        {
            var gymEntity = await _repository.Gym.GetGymAsync(subscriptionTypeForCreationDto.GymId, false);
            if (gymEntity is null)
                throw new GymNotFoundException($"Gym with id {subscriptionTypeForCreationDto.GymId} does not exist in the databse");

            var subscriptionTypeToCreate = new SubscriptionType
            {
                Id = Guid.NewGuid(),
                GymId = subscriptionTypeForCreationDto.GymId,
                Name = subscriptionTypeForCreationDto.Name,
                Price = subscriptionTypeForCreationDto.Price,
                ValidityInDays = subscriptionTypeForCreationDto.ValidityInDays,
                MaxAllowedTrainingSessions = subscriptionTypeForCreationDto.MaxAllowedTrainingSessions,
                MaxAllowedDailyTrainingSessions = subscriptionTypeForCreationDto.MaxAllowedDailyTrainingSessions
            };

            _repository.SubscriptionType.CreateSubscriptionType(subscriptionTypeToCreate);
            await _repository.SaveAsync();
        }

        public async Task DeleteSubscriptionType(Guid subscriptionTypeId)
        {
            var subscriptionTypeEntity = await _repository.SubscriptionType.GetSubscriptionTypeAsync(subscriptionTypeId, true);
            if (subscriptionTypeEntity is null)
                throw new SubscriptionTypeNotFoundException($"Subscription type with id {subscriptionTypeId} does not exist in the database");

            _repository.SubscriptionType.DeleteSubscriptionType(subscriptionTypeEntity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<SubscriptionTypeForReturnDto>> GetAllSubscriptionTypesAsync()
        {
            var subscriptionTypeEntities = await _repository.SubscriptionType.GetAllSubscriptionTypesAsync(false);

            List<SubscriptionTypeForReturnDto> res = new();
            foreach (var subscriptionTypeEntity in subscriptionTypeEntities)
            {
                var subsciprtionTypeToAppend = new SubscriptionTypeForReturnDto
                {
                    Id = subscriptionTypeEntity.Id,
                    GymId = subscriptionTypeEntity.GymId,
                    Name = subscriptionTypeEntity.Name,
                    Price = subscriptionTypeEntity.Price,
                    ValidityInDays = subscriptionTypeEntity.ValidityInDays,
                    MaxAllowedTrainingSessions = subscriptionTypeEntity.MaxAllowedTrainingSessions,
                    MaxAllowedDailyTrainingSessions = subscriptionTypeEntity.MaxAllowedDailyTrainingSessions
                };
                res.Add(subsciprtionTypeToAppend);
            }

            return res;
        }

        public async Task<IEnumerable<SubscriptionTypeForReturnDto>> GetAllSubscriptionTypesForGymAsync(Guid gymId)
        {
            var gymEntity = await _repository.Gym.GetGymAsync(gymId, false);
            if (gymEntity is null)
                throw new GymNotFoundException($"Gym with id {gymId} does not exist in the database");

            var subscriptionTypeEntities = await _repository.SubscriptionType.GetAllSubscriptionTypesForGymAsync(gymId, false);
            List<SubscriptionTypeForReturnDto> res = new();
            foreach (var subscriptionTypeEntity in subscriptionTypeEntities)
            {
                var subscriptionTypeToReturn = new SubscriptionTypeForReturnDto
                {
                    Id = subscriptionTypeEntity.Id,
                    GymId = subscriptionTypeEntity.GymId,
                    Name = subscriptionTypeEntity.Name,
                    Price = subscriptionTypeEntity.Price,
                    ValidityInDays = subscriptionTypeEntity.ValidityInDays,
                    MaxAllowedTrainingSessions = subscriptionTypeEntity.MaxAllowedTrainingSessions,
                    MaxAllowedDailyTrainingSessions = subscriptionTypeEntity.MaxAllowedDailyTrainingSessions
                };
                res.Add(subscriptionTypeToReturn);
            }

            return res;
        }

        public async Task<SubscriptionTypeForReturnDto?> GetSubscriptionTypeAsync(Guid subscriptionTypeId)
        {
            var subscriptionTypeEntity = await _repository.SubscriptionType.GetSubscriptionTypeAsync(subscriptionTypeId, false);
            if (subscriptionTypeEntity is null)
                throw new SubscriptionTypeNotFoundException($"Subscription type with id {subscriptionTypeId} does not exist in the database");

            var subscriptionTypeToReturn = new SubscriptionTypeForReturnDto
            {
                Id = subscriptionTypeEntity.Id,
                GymId = subscriptionTypeEntity.GymId,
                Name = subscriptionTypeEntity.Name,
                Price = subscriptionTypeEntity.Price,
                ValidityInDays = subscriptionTypeEntity.ValidityInDays,
                MaxAllowedTrainingSessions = subscriptionTypeEntity.MaxAllowedTrainingSessions,
                MaxAllowedDailyTrainingSessions = subscriptionTypeEntity.MaxAllowedDailyTrainingSessions
            };

            return subscriptionTypeToReturn;
        }

        public async Task UpdateSubscriptionType(Guid subscriptionTypeId, SubscriptionTypeForUpdateDto subscriptionTypeForUpdateDto)
        {
            var subscriptionTypeEntity = await _repository.SubscriptionType.GetSubscriptionTypeAsync(subscriptionTypeId, true);
            if (subscriptionTypeEntity is null)
                throw new SubscriptionTypeNotFoundException($"Subscription type with id {subscriptionTypeId} does not exist in the database");

            subscriptionTypeEntity.Name = subscriptionTypeForUpdateDto.Name;
            subscriptionTypeEntity.Price = subscriptionTypeForUpdateDto.Price;
            subscriptionTypeEntity.ValidityInDays = subscriptionTypeForUpdateDto.ValidityInDays;
            subscriptionTypeEntity.MaxAllowedTrainingSessions = subscriptionTypeForUpdateDto.MaxAllowedTrainingSessions;
            subscriptionTypeEntity.MaxAllowedDailyTrainingSessions = subscriptionTypeForUpdateDto.MaxAllowedDailyTrainingSessions;

            _repository.SubscriptionType.UpdateSubscriptionType(subscriptionTypeEntity);
            await _repository.SaveAsync();
        }
    }
}
