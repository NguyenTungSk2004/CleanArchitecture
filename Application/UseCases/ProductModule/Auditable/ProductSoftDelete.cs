using Application.UseCases.BaseAuditable.SoftDelete;
using Domain.ProductModule.Entities;
using SharedKernel.Interfaces;

namespace Application.UseCases.ProductModule.Auditable
{
    public record ProductSoftDeleteCommand(List<long> Ids, long UserId) : GenericSoftDeleteCommand(Ids, UserId);
    public class ProductSoftDeleteHandler : GenericSoftDeleteHandler<Product, ProductSoftDeleteCommand>
    {
        public ProductSoftDeleteHandler(IRepository<Product> repository) : base(repository) { }
    }
}