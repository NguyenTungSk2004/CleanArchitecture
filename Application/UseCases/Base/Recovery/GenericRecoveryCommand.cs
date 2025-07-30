using MediatR;

namespace Application.UseCases.Base.Recovery
{
    public abstract record GenericRecoveryCommand(long Id, long UserId) : IRequest<bool>;
}