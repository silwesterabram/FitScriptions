using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? IdCardNumber { get; set; }
        public string? Address { get; set; }
        public string? BarcodeUrl { get; set; }
    }
}
