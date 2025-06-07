using HaiphongTech.Domain.Entities.Products.ProductAggregate;
using HaiphongTech.Domain.Repositories;
using HaiphongTech.Infrastructure.Persistence;

namespace HaiphongTech.Infrastructure.Repositories.Products
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}