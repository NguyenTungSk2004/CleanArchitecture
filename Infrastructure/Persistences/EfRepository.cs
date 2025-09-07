namespace Infrastructure.Persistence;

using Ardalis.Specification.EntityFrameworkCore;
using SharedKernel.Base;
using SharedKernel.Interfaces;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : Entity, IAggregateRoot
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

public class EfReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : Entity
{
    public EfReadRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
