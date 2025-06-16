using HaiphongTech.SharedKernel.Interfaces;
using MediatR;

namespace HaiphongTech.Application.Services.BaseServices.HardDelete
{
    public record HardDeleteCommand<TEntity>(List<int> Ids, int UserId) : IRequest<bool>
        where TEntity : class, IAuditable;
}