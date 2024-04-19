using Entities;
using Microsoft.AspNetCore.Identity;
using QRCoder;
using Service.Contracts;
using Shared.Exceptions;

namespace Service
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;

        public ProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<byte[]> GenerateQRCode(Guid id)
        {
            var profileEntity = await _userManager.FindByIdAsync(id.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {id} does not exist in the database");

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = 
                qRCodeGenerator.CreateQrCode(
                    $"{profileEntity.UserName}\n" +
                    $"{profileEntity.IdCardNumber}\n" +
                    $"{profileEntity.PhoneNumber}\n" +
                    $"{profileEntity.Email}", 
                    QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qRCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }

        public async Task<string?> GetProfilePictureUriAsync(Guid id)
        {
            var profileEntity = await _userManager.FindByIdAsync(id.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {id} does not exist in the database");

            return profileEntity.ProfilePictureUrl;
        }

        public async Task<string?> GetQRCodeUriAsync(Guid id)
        {
            var profileEntity = await _userManager.FindByIdAsync(id.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {id} does not exist in the database");

            return profileEntity.BarcodeUrl;
        }

        public async Task SaveProfilePictureUri(Guid id, string? profilePictureUri)
        {
            var profileEntity = await _userManager.FindByIdAsync(id.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {id} does not exist in the database");

            profileEntity.ProfilePictureUrl = profilePictureUri;
            await _userManager.UpdateAsync(profileEntity);
        }

        public async Task SaveProfileQRCodeUri(Guid id, string? qrCodeUri)
        {
            var profileEntity = await _userManager.FindByIdAsync(id.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {id} does not exist in the database");

            profileEntity.BarcodeUrl = qrCodeUri;
            await _userManager.UpdateAsync(profileEntity);
        }
    }
}
