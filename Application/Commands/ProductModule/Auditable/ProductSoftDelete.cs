using Application.Commands.BaseAuditable.SoftDelete;
using Domain.ProductModule.Entities;
using SharedKernel.Interfaces;

namespace Application.Commands.ProductModule.Auditable
{
    public record ProductSoftDeleteCommand(List<long> Ids, long UserId) : GenericSoftDeleteCommand(Ids, UserId);
    public class ProductSoftDeleteHandler : GenericSoftDeleteHandler<Product, ProductSoftDeleteCommand>
    {
        public ProductSoftDeleteHandler(IRepository<Product> repository) : base(repository) { }
    }
}