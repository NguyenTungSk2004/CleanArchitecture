using MediatR;

namespace Application.UseCases.BaseServices.HardDelete
{
    public abstract record GenericHardDeleteCommand(List<int> Ids, int UserId) : IRequest<bool>;
}