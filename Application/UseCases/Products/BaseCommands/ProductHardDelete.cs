using Application.UseCases.BaseServices.HardDelete;
using Domain.Entities.ProductModule.ProductAggregate;
using SharedKernel.Interfaces;

namespace Application.UseCases.Products.BaseCommands
{
    public record ProductHardDeleteCommand(List<int> Ids, int UserId) : GenericHardDeleteCommand(Ids, UserId);

    public class ProductHardDeleteHandler : GenericHardDeleteHandler<Product, ProductHardDeleteCommand>
    {
        public ProductHardDeleteHandler(IRepository<Product> repository) : base(repository) { }
    }   
}