using Ardalis.Specification;

namespace HaiphongTech.SharedKernel.Interfaces;
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
    IQueryable<T> Table { get; }
}
