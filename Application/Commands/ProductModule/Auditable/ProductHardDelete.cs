using Application.Commands.BaseAuditable.HardDelete;
using Domain.ProductModule.Entities;
using SharedKernel.Interfaces;

namespace Application.Commands.ProductModule.Auditable
{
    public record ProductHardDeleteCommand(List<long> Ids, long UserId) : GenericHardDeleteCommand(Ids, UserId);

    public class ProductHardDeleteHandler : GenericHardDeleteHandler<Product, ProductHardDeleteCommand>
    {
        public ProductHardDeleteHandler(IRepository<Product> repository) : base(repository) { }
    }   
}