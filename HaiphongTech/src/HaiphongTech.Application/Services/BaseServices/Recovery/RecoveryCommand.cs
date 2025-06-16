using HaiphongTech.SharedKernel.Base;
using HaiphongTech.SharedKernel.Interfaces;
using MediatR;

namespace HaiphongTech.Application.Services.BaseServices.Recovery
{
    public record RecoveryCommand<TEntity>(int Id, int UserId) : IRequest<bool>
        where TEntity : class, IAuditable;
}