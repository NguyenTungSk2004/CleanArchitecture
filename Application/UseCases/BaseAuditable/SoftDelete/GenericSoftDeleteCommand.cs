using MediatR;

namespace Application.UseCases.BaseAuditable.SoftDelete
{
    public abstract record GenericSoftDeleteCommand(List<long> Ids, long UserId) : IRequest<bool>;
}