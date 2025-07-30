using MediatR;

namespace Application.UseCases.Base.SoftDelete
{
    public abstract record GenericSoftDeleteCommand(List<long> Ids, long UserId) : IRequest<bool>;
}