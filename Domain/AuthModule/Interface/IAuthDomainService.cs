using Domain.AuthModule.AggregateRoot;

namespace Domain.AuthModule.Interface
{
    public interface IAuthDomainService
    {
        void CreateAccount(User user, string userName, string password);
    }
}