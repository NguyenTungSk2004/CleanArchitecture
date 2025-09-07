using MediatR;

namespace Application.Commands.BaseAuditable.HardDelete
{
    public abstract record GenericHardDeleteCommand(List<long> Ids, long UserId) : IRequest<bool>;
}