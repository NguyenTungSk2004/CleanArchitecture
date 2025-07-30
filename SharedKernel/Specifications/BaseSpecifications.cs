using Ardalis.Specification;
using SharedKernel.Base;
using SharedKernel.Interfaces;

namespace SharedKernel.Specifications
{
    public class EntitiesByIdsSpecification<TEntity> : Specification<TEntity>
        where TEntity : Entity, IAggregateRoot, ISoftDeletable
    {
        public EntitiesByIdsSpecification(IEnumerable<long> ids, bool? isDeleted = null)
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