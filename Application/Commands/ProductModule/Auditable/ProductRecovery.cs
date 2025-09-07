using Application.Commands.BaseAuditable.Recovery;
using Domain.ProductModule.Entities;
using SharedKernel.Interfaces;

namespace Application.Commands.ProductModule.Auditable
{
    public record ProductRecoveryCommand(long Id, long UserId) : GenericRecoveryCommand(Id, UserId);

    public class ProductRecoveryHandler : GenericRecoveryHandler<Product, ProductRecoveryCommand>
    {
        public ProductRecoveryHandler(IRepository<Product> repository) : base(repository) { }
    }
}