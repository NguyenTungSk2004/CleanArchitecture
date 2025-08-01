using SharedKernel.Base;

namespace Domain.AuthModule.Entities
{
    public class Account: Entity
    {
        public long UserId { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string? Salt { get; private set; }
        public Account(long userId, string userName, string password, string? salt = null)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Salt = salt; 
        }
        public void ChangePassword(string newPassword, string? newSalt = null)
        {
            Password = newPassword;
            Salt = newSalt;
        }
    }
}