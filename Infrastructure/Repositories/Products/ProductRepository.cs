using Domain.Entities.Products.ProductAggregate;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories.Products
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}