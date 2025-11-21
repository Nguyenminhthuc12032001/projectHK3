using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Domain.Emtitys
{
    internal class User : BaseEntity
    {
        public string? FullName { get; set; }
        public EmailAddress? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public Address? Address { get; set; }
    }
}
