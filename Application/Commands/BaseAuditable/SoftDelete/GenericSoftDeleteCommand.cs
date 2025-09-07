using MediatR;

namespace Application.Commands.BaseAuditable.SoftDelete
{
    public abstract record GenericSoftDeleteCommand(List<long> Ids, long UserId) : IRequest<bool>;
}