using Application.UseCases.BaseServices.SoftDelete;
using Domain.Entities.ProductModule.ProductAggregate;
using SharedKernel.Interfaces;

namespace Application.UseCases.Products.BaseCommands
{
    public record ProductSoftDeleteCommand(List<int> Ids, int UserId) : GenericSoftDeleteCommand(Ids, UserId);
    public class ProductSoftDeleteHandler : GenericSoftDeleteHandler<Product, ProductSoftDeleteCommand>
    {
        public ProductSoftDeleteHandler(IRepository<Product> repository) : base(repository) { }
    }
}