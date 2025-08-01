using SharedKernel.Base;

namespace Domain.AuthModule.Entities
{
    public class UserSessions : Entity
    {
        public int UserId { get; private set; }
        public string RefreshToken { get; private set; } = string.Empty;
        public string DeviceInfo { get; private set; } = string.Empty;
        public string IPAddress { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public bool Revoked { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public DateTime? LastUsedAt { get; private set; }
    }
}