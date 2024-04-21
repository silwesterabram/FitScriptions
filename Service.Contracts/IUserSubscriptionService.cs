using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IUserSubscriptionService
    {
        Task<UserSubscriptionForReturnDto> GetUserSubscriptionByIdAsync(Guid userSubscriptionId);
        Task<List<UserSubscriptionForReturnDto>> GetUserSubscriptionsAsync();
        Task<List<UserSubscriptionForReturnDto>> GetUserSubscriptionsByUserIdAsync(Guid userId);
        Task CreateUserSubscription(UserSubscriptionForCreationDto userSubscriptionForCreationDto);
        Task<string?> GetQRCodeUrlAsync(Guid userSubscriptionId);
        Task<byte[]> GenerateQRCode(Guid userSubscriptionId);
        Task SaveQRCodeUrl(Guid userSubscriptionId, string qrCodeUrl);
        Task DeleteUserSubscription(Guid userSubscriptionId);
    }
}
