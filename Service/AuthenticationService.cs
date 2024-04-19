using Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;

        public AuthenticationService(ILoggerManager logger, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(); 
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user!.UserName!),
                new Claim(ClaimTypes.NameIdentifier, _user.Id)
            };

            var roles = await _userManager.GetRolesAsync(_user!);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var userToCreate = new User
            {
                FirstName = userForRegistrationDto.FirstName,
                LastName = userForRegistrationDto.LastName,
                UserName = userForRegistrationDto.UserName,
                ProfilePictureUrl = "",
                IdCardNumber = userForRegistrationDto.IdCardNumber,
                Address = userForRegistrationDto.Address,
                BarcodeUrl = "",
                Email = userForRegistrationDto.Email,
                PhoneNumber = userForRegistrationDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(userToCreate, userForRegistrationDto.Password!);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(userToCreate, ["User"]);

            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth) 
        { 
            _user = await _userManager.FindByNameAsync(userForAuth.UserName!);
            if (_user is null)
                throw new UserNotFoundException($"User with user name {userForAuth.UserName} not found.");

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password!));

            if (!result) 
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password."); 

            return result; 
        }
    }
}
