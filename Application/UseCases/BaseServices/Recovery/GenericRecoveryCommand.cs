using MediatR;

namespace Application.UseCases.BaseServices.Recovery
{
    public abstract record GenericRecoveryCommand(int Id, int UserId) : IRequest<bool>;
}