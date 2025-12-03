using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.Abstractions
{
    public interface ICurrentUserService
    {
        public string? Email { get; }
        public string? Role { get; }
    }
}
