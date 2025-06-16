using Ardalis.Specification;
using HaiphongTech.SharedKernel.Interfaces;

namespace HaiphongTech.SharedKernel.Specifications
{
    public class EntitiesByIdsSpecification<TEntity> : Specification<TEntity>
        where TEntity : class, IAuditable, IAggregateRoot
    {
        public EntitiesByIdsSpecification(IEnumerable<int> ids, bool? isDeleted = null)
        {
            if (isDeleted.HasValue)
            {
                Query.Where(e => ids.Contains(e.Id) && e.IsDeleted == isDeleted.Value);
            }
            else
            {
                Query.Where(e => ids.Contains(e.Id));
            }
        }
    }
}