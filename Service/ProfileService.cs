using Entities;
using Microsoft.AspNetCore.Identity;
using QRCoder;
using Service.Contracts;
using Shared.DataTransferObjects;
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

        public async Task DeleteProfile(Guid id)
        {
            var profileEntity = await _userManager.FindByIdAsync(id.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {id} does not exist in the database");

            await _userManager.DeleteAsync(profileEntity);
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

        public IEnumerable<ProfileForReturnDto> GetAllProfiles()
        {
            var profileEntities = _userManager.Users;

            List<ProfileForReturnDto> res = new();
            foreach (var profileEntity in profileEntities)
            {
                var profileToAppend = new ProfileForReturnDto
                {
                    Id = new Guid(profileEntity.Id),
                    FirstName = profileEntity.FirstName,
                    LastName = profileEntity.LastName,
                    UserName = profileEntity.UserName,
                    Email = profileEntity.Email,
                    PhoneNumber = profileEntity.PhoneNumber,
                    Address = profileEntity.Address,
                    IdCardNumber = profileEntity.IdCardNumber,
                    ProfilePictureUrl = profileEntity.ProfilePictureUrl,
                    BarcodeUrl = profileEntity.BarcodeUrl
                };

                res.Add(profileToAppend);
            }

            return res;
        }

        public async Task<ProfileForReturnDto> GetProfileAsync(Guid id)
        {
            var profileEntity = await _userManager.FindByIdAsync(id.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {id} does not exist in the database");

            return new ProfileForReturnDto
            {
                Id = new Guid(profileEntity.Id),
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                UserName = profileEntity.UserName,
                Email = profileEntity.Email,
                PhoneNumber = profileEntity.PhoneNumber,
                Address = profileEntity.Address,
                IdCardNumber = profileEntity.IdCardNumber,
                ProfilePictureUrl = profileEntity.ProfilePictureUrl,
                BarcodeUrl = profileEntity.BarcodeUrl
            };
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

        public async Task UpdateProfile(Guid userId, ProfileForUpdateDto profileForUpdateDto)
        {
            var profileEntity = await _userManager.FindByIdAsync(userId.ToString());
            if (profileEntity is null)
                throw new UserNotFoundException($"User with id {userId} does not exist in the database");

            profileEntity.FirstName = profileForUpdateDto.FirstName;
            profileEntity.LastName = profileForUpdateDto.LastName;
            profileEntity.UserName = profileForUpdateDto.UserName;
            profileEntity.Email = profileForUpdateDto.Email;
            profileEntity.PhoneNumber = profileForUpdateDto.PhoneNumber;
            profileEntity.Address = profileForUpdateDto.Address;
            profileEntity.IdCardNumber = profileForUpdateDto.IdCardNumber;

            await _userManager.UpdateAsync(profileEntity);
        }
    }
}
