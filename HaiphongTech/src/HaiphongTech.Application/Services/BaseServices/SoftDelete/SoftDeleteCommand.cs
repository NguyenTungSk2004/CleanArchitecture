using HaiphongTech.SharedKernel.Interfaces;
using MediatR;

namespace HaiphongTech.Application.Services.BaseServices.SoftDelete
{
    public record SoftDeleteCommand<TEntity>(List<int> Ids, int UserId) : IRequest<bool>
        where TEntity : class, IAuditable;

}