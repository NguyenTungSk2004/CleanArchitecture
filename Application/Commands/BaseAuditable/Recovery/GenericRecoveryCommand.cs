using MediatR;

namespace Application.Commands.BaseAuditable.Recovery
{
    public abstract record GenericRecoveryCommand(long Id, long UserId) : IRequest<bool>;
}