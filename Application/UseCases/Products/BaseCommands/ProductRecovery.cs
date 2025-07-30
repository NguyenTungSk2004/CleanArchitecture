using Application.UseCases.Base.Recovery;
using Domain.ProductModule.Entities;
using SharedKernel.Interfaces;

namespace Application.UseCases.Products.BaseCommands
{
    public record ProductRecoveryCommand(long Id, long UserId) : GenericRecoveryCommand(Id, UserId);

    public class ProductRecoveryHandler : GenericRecoveryHandler<Product, ProductRecoveryCommand>
    {
        public ProductRecoveryHandler(IRepository<Product> repository) : base(repository) { }
    }
}