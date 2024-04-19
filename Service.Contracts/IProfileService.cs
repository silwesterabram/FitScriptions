using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IProfileService
    {
        Task<string?> GetProfilePictureUriAsync(Guid id);
        Task SaveProfilePictureUri(Guid id, string? profilePictureUri);
        Task<byte[]> GenerateQRCode(Guid id);
        Task<string?> GetQRCodeUriAsync(Guid id);
        Task SaveProfileQRCodeUri(Guid id, string? qrCodeUri);
        Task<ProfileForReturnDto> GetProfileAsync(Guid id);
        IEnumerable<ProfileForReturnDto> GetAllProfiles();
        Task DeleteProfile(Guid id);
        Task UpdateProfile(Guid userId, ProfileForUpdateDto profileForUpdateDto);
    }
}
