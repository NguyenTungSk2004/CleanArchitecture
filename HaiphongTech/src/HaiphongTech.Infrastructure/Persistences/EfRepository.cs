namespace HaiphongTech.Infrastructure.Persistence;

using Ardalis.Specification.EntityFrameworkCore;
using HaiphongTech.SharedKernel.Interfaces;

public abstract class EfRepository<T> : RepositoryBase<T> where T : class, IAggregateRoot
{
    AppDbContext _dbContext;
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    private IQueryable<T>? _entities;
    public virtual IQueryable<T> Table => _entities ?? (_entities = _dbContext.Set<T>());
}
