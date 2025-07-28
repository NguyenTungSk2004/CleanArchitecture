namespace Infrastructure.Persistence;

using Ardalis.Specification.EntityFrameworkCore;
using SharedKernel.Interfaces;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T>  where T : class, IAggregateRoot
{
    AppDbContext _dbContext;
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    private IQueryable<T>? _entities;
    public virtual IQueryable<T> Table => _entities ?? (_entities = _dbContext.Set<T>());
}
