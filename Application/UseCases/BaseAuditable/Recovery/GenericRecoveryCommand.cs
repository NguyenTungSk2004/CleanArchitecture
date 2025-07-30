using MediatR;

namespace Application.UseCases.BaseAuditable.Recovery
{
    public abstract record GenericRecoveryCommand(long Id, long UserId) : IRequest<bool>;
}