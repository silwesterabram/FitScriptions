using Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using QRCoder;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace Service
{
    internal sealed class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;

        public UserSubscriptionService(IRepositoryManager repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task CreateUserSubscription(UserSubscriptionForCreationDto userSubscriptionForCreationDto)
        {
            var userEntity = await _userManager.FindByIdAsync(userSubscriptionForCreationDto.UserId.ToString());
            if (userEntity is null)
                throw new UserNotFoundException($"User by id {userSubscriptionForCreationDto.UserId} does not exist in the database.");

            var subscriptionTypeEntity = await _repository.SubscriptionType.GetSubscriptionTypeAsync(userSubscriptionForCreationDto.SubscriptionTypeId, trackChanges: false);
            if (subscriptionTypeEntity is null)
                throw new SubscriptionTypeNotFoundException($"Subscription type by id {userSubscriptionForCreationDto.SubscriptionTypeId} does not exist in the database.");

            var userSubscriptionToCreate = new UserSubscription
            {
                Id = Guid.NewGuid(),
                UserId = userSubscriptionForCreationDto.UserId.ToString(),
                SubscriptionTypeId = userSubscriptionForCreationDto.SubscriptionTypeId,
                PurchaseTime = userSubscriptionForCreationDto.PurchaseTime,
                BarcodeUrl = "",
                TrainingSessionsDone = 0,
                PurchasePrice = subscriptionTypeEntity.Price,
                IsActive = true
            };

            _repository.UserSubscription.CreateUserSubscriptionAsync(userSubscriptionToCreate);
            await _repository.SaveAsync();
        }

        public async Task DeleteUserSubscription(Guid userSubscriptionId)
        {
            var userSubscriptionEntity = await _repository.UserSubscription.GetUserSubscriptionAsync(userSubscriptionId, trackChanges: true);
            if (userSubscriptionEntity is null)
                throw new UserSubscriptionNotFoundException($"User subscription by id {userSubscriptionId} does not exist in the database.");

            _repository.UserSubscription.DeleteUserSubscriptionAsync(userSubscriptionEntity);
            await _repository.SaveAsync();
        }

        public async Task<byte[]> GenerateQRCode(Guid userSubscriptionId)
        {
            var userSubscriptionEntity = await _repository.UserSubscription.GetUserSubscriptionAsync(userSubscriptionId, trackChanges: false);
            if (userSubscriptionEntity is null)
                throw new UserSubscriptionNotFoundException($"User subscription by id {userSubscriptionId} does not exist in the database.");

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData =
                qRCodeGenerator.CreateQrCode(
                    $"active={userSubscriptionEntity.IsActive}\n" +
                    $"purchaseTime={userSubscriptionEntity.PurchaseTime}\n",
                    QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qRCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }

        public async Task<string?> GetQRCodeUrlAsync(Guid userSubscriptionId)
        {
            var userSubscriptionEntity = await _repository.UserSubscription.GetUserSubscriptionAsync(userSubscriptionId, trackChanges: false);
            if (userSubscriptionEntity is null)
                throw new UserSubscriptionNotFoundException($"User subscription by id {userSubscriptionId} does not exist in the database.");

            return userSubscriptionEntity.BarcodeUrl;
        }

        public async Task<UserSubscriptionForReturnDto> GetUserSubscriptionByIdAsync(Guid userSubscriptionId)
        {
            var userSubscriptionEntity = await _repository.UserSubscription.GetUserSubscriptionAsync(userSubscriptionId, trackChanges: false);
            if (userSubscriptionEntity is null)
                throw new UserSubscriptionNotFoundException($"User subscription by id {userSubscriptionId} does not exist in the database.");

            return new UserSubscriptionForReturnDto
            {
                Id = userSubscriptionEntity.Id,
                UserId = new Guid(userSubscriptionEntity.UserId!),
                SubscriptionTypeId = userSubscriptionEntity.SubscriptionTypeId,
                PurchasePrice = userSubscriptionEntity.PurchasePrice,
                BarcodeUrl = userSubscriptionEntity.BarcodeUrl,
                TrainingSessionsDone = userSubscriptionEntity.TrainingSessionsDone,
                PurchaseTime = userSubscriptionEntity.PurchaseTime,
                IsActive = userSubscriptionEntity.IsActive,
            };
        }

        public async Task<List<UserSubscriptionForReturnDto>> GetUserSubscriptionsAsync()
        {
            var userSubscriptionEntities = await _repository.UserSubscription.GetAllUserSubscriptionsAsync(trackChanges: false);

            List<UserSubscriptionForReturnDto> res = new();
            foreach (var userSubscriptionEntity in userSubscriptionEntities)
            {
                var toAppend = new UserSubscriptionForReturnDto
                {
                    Id = userSubscriptionEntity.Id,
                    UserId = new Guid(userSubscriptionEntity.UserId!),
                    SubscriptionTypeId = userSubscriptionEntity.SubscriptionTypeId,
                    PurchasePrice = userSubscriptionEntity.PurchasePrice,
                    BarcodeUrl = userSubscriptionEntity.BarcodeUrl,
                    TrainingSessionsDone = userSubscriptionEntity.TrainingSessionsDone,
                    PurchaseTime = userSubscriptionEntity.PurchaseTime,
                    IsActive = userSubscriptionEntity.IsActive,
                };
                res.Add(toAppend);
            }

            return res;
        }

        public async Task<List<UserSubscriptionForReturnDto>> GetUserSubscriptionsByUserIdAsync(Guid userId)
        {
            var userEntity = await _userManager.FindByIdAsync(userId.ToString());
            if (userEntity is null)
                throw new UserNotFoundException($"User by id {userId} does not exist in the database.");

            var userSubscriptionEntities = await _repository.UserSubscription.GetAllUserSubscriptionsByUserId(userId, trackChanges: false);

            List<UserSubscriptionForReturnDto> res = new();
            foreach (var userSubscriptionEntity in userSubscriptionEntities)
            {
                var toAppend = new UserSubscriptionForReturnDto
                {
                    Id = userSubscriptionEntity.Id,
                    UserId = new Guid(userSubscriptionEntity.UserId!),
                    SubscriptionTypeId = userSubscriptionEntity.SubscriptionTypeId,
                    PurchasePrice = userSubscriptionEntity.PurchasePrice,
                    BarcodeUrl = userSubscriptionEntity.BarcodeUrl,
                    TrainingSessionsDone = userSubscriptionEntity.TrainingSessionsDone,
                    PurchaseTime = userSubscriptionEntity.PurchaseTime,
                    IsActive = userSubscriptionEntity.IsActive,
                };
                res.Add(toAppend);
            }

            return res;
        }

        public async Task SaveQRCodeUrl(Guid userSubscriptionId, string qrCodeUrl)
        {
            var userSubscriptionEntity = await _repository.UserSubscription.GetUserSubscriptionAsync(userSubscriptionId, trackChanges: true);
            if (userSubscriptionEntity is null)
                throw new UserSubscriptionNotFoundException($"User subscription by id {userSubscriptionId} does not exist in the database.");

            userSubscriptionEntity.BarcodeUrl = qrCodeUrl;
            _repository.UserSubscription.UpdateUserSubscriptionAsync(userSubscriptionEntity);
            await _repository.SaveAsync();
        }
    }
}
