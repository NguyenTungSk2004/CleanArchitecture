using Domain.AuthModule.Enum;
using SharedKernel.Base;

namespace Domain.AuthModule.Entities
{
    public class UserProviders : Entity
    {
        public int UserId { get; private set; }
        public ProviderType ProviderType { get; private set; }
        public int? ProviderId { get; private set; }
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? AccessToken { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? IsVerified { get; private set; }
    
    }
}