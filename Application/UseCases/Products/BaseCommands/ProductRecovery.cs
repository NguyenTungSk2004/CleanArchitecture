using Application.UseCases.BaseServices.Recovery;
using Domain.Entities.ProductModule.ProductAggregate;
using SharedKernel.Interfaces;

namespace Application.UseCases.Products.BaseCommands
{
    public record ProductRecoveryCommand(int Id, int UserId) : GenericRecoveryCommand(Id, UserId);

    public class ProductRecoveryHandler : GenericRecoveryHandler<Product, ProductRecoveryCommand>
    {
        public ProductRecoveryHandler(IRepository<Product> repository) : base(repository) { }
    }
}