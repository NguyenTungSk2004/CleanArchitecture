using MediatR;

namespace Application.UseCases.Base.HardDelete
{
    public abstract record GenericHardDeleteCommand(List<long> Ids, long UserId) : IRequest<bool>;
}