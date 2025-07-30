using Domain.AuthModule.AggregateRoot;
using Domain.AuthModule.Commons.PasswordHasher;
using Domain.AuthModule.Entities;
using Domain.AuthModule.Interface;

namespace Domain.AuthModule.Services
{
    public class AuthDomainService : IAuthDomainService
    {
        private readonly IPasswordHasher _passwordHasher;

        public AuthDomainService(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public void CreateAccount(User user, string userName, string password)
        {
            if (user.Account != null)
                throw new InvalidOperationException("User already has authentication.");
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.");

            var (hash, salt) = _passwordHasher.HashPassword(password);
            user.Account = new Account(user.Id, userName, hash, salt);
        }
    }

}