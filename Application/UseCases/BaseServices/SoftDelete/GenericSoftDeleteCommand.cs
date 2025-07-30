using MediatR;

namespace Application.UseCases.BaseServices.SoftDelete
{
    public abstract record GenericSoftDeleteCommand(List<int> Ids, int UserId) : IRequest<bool>;
}