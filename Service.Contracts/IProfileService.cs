namespace Service.Contracts
{
    public interface IProfileService
    {
        Task<string?> GetProfilePictureUriAsync(Guid id);
        Task SaveProfilePictureUri(Guid id, string? profilePictureUri);
        Task<byte[]> GenerateQRCode(Guid id);
        Task<string?> GetQRCodeUriAsync(Guid id);
        Task SaveProfileQRCodeUri(Guid id, string? qrCodeUri);
    }
}
