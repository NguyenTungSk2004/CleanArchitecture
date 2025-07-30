using MediatR;

namespace Application.UseCases.BaseAuditable.HardDelete
{
    public abstract record GenericHardDeleteCommand(List<long> Ids, long UserId) : IRequest<bool>;
}