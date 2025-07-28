using SharedKernel.Base;
using SharedKernel.Interfaces;
using MediatR;

namespace Application.Services.BaseServices.Recovery
{
    public record RecoveryCommand<TEntity>(int Id, int UserId) : IRequest<bool>
        where TEntity : class, IAuditable;
}