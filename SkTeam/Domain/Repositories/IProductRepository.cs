using Domain.Entities.Products.ProductAggregate;
using SharedKernel.Interfaces;

namespace Domain.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
         
    }
}