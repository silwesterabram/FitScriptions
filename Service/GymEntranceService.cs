using Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;
using System.Runtime.InteropServices;

namespace Service
{
    internal sealed class GymEntranceService : IGymEntranceService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;

        public GymEntranceService(IRepositoryManager repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task CreateGymEntrance(GymEntraceForCreationDto gymEntranceForCreationDto)
        {
            var userEntity = await _userManager.FindByIdAsync(gymEntranceForCreationDto.UserId.ToString());
            if (userEntity is null)
                throw new UserNotFoundException($"User by id {gymEntranceForCreationDto.UserId} does not exist in the database.");

            var gymEntity = await _repository.Gym.GetGymAsync(gymEntranceForCreationDto.GymId, trackChanges: false);
            if (gymEntity is null)
                throw new GymNotFoundException($"Gym by id {gymEntranceForCreationDto.GymId} does not exist in the database.");

            var allUserSubscriptions = await _repository.UserSubscription.GetAllUserSubscriptionsByUserId(gymEntranceForCreationDto.UserId, trackChanges: false);
            int activeSubscriptionCount = 0;

            foreach (var subscription in allUserSubscriptions)
            {
                if (subscription.IsActive)
                    activeSubscriptionCount++;
            }

            if (activeSubscriptionCount == 0)
                throw new NoActiveSubscriptionsException($"User by id {gymEntranceForCreationDto.UserId} does not have any active subscriptions.");

            var subscriptionEntity = await _repository.UserSubscription.GetUserSubscriptionAsync(gymEntranceForCreationDto.UserSubscriptionId, trackChanges: true);
            if (subscriptionEntity is null)
                throw new UserSubscriptionNotFoundException($"User subscription by id {gymEntranceForCreationDto.UserSubscriptionId} does not exist in the database.");

            
            var subscriptionTypeEntity = await _repository.SubscriptionType.GetSubscriptionTypeAsync(subscriptionEntity.SubscriptionTypeId, trackChanges: false);
            if (subscriptionTypeEntity is null)
                throw new SubscriptionTypeNotFoundException($"Subscription type by id {subscriptionEntity.SubscriptionTypeId} does not exist in the database.");

            if (subscriptionTypeEntity.GymId != gymEntity.Id)
                throw new IncorrectUserSubscriptionToGymCorrespondenceException($"User subscription by id {gymEntranceForCreationDto.UserSubscriptionId} does not correspond to gym by id {gymEntranceForCreationDto.GymId}.");

            subscriptionEntity.TrainingSessionsDone++;

            if (subscriptionEntity.TrainingSessionsDone >= subscriptionTypeEntity.MaxAllowedTrainingSessions)
            {
                subscriptionEntity.IsActive = false;
                await _repository.SaveAsync();
            }

            

            var entranceToCreate = new GymEntrance
            {
                Id = Guid.NewGuid(),
                UserId = gymEntranceForCreationDto.UserId.ToString(),
                GymId = gymEntranceForCreationDto.GymId,
                UserSubscriptionId = gymEntranceForCreationDto.UserSubscriptionId,
                EntranceTime = DateTime.Now,
                BarcodeUrl = "",
            };

            _repository.GymEntrance.CreateEntrance(entranceToCreate);
            await _repository.SaveAsync();
        }

        public async Task<GymEntranceForReturnDto> GetGymEntranceByIdAsync(Guid gymEntranceId)
        {
            var gymEntranceEntity = await _repository.GymEntrance.GetGymEntranceByIdAsync(gymEntranceId, trackChanges: false);
            if (gymEntranceEntity is null)
                throw new GymEntranceNotFoundException($"Gym entrance by id {gymEntranceId} does not exist in the database.");

            var gymEntranceToReturn = new GymEntranceForReturnDto
            {
                Id = gymEntranceEntity.Id,
                UserId = new Guid(gymEntranceEntity.UserId!),
                GymId = gymEntranceEntity.GymId,
                EntranceTime = gymEntranceEntity.EntranceTime,
                BarcodeUrl = gymEntranceEntity.BarcodeUrl,
            };

            return gymEntranceToReturn;
        }

        public async Task<List<GymEntranceForReturnDto>> GetGymEntrancesByUserIdAsync(Guid userId)
        {
            var gymEntranceEntities = await _repository.GymEntrance.GetEnrtancesByUserId(userId, trackChanges: false);

            List<GymEntranceForReturnDto> res = new();
            foreach (var gymEntranceEntity in gymEntranceEntities)
            {
                var toAppend = new GymEntranceForReturnDto
                {
                    Id = gymEntranceEntity.Id,
                    UserId = new Guid(gymEntranceEntity.UserId!),
                    GymId = gymEntranceEntity.GymId,
                    EntranceTime = gymEntranceEntity.EntranceTime,
                    BarcodeUrl = gymEntranceEntity.BarcodeUrl,
                };
                res.Add(toAppend);
            }

            return res;
        }
    }
}
