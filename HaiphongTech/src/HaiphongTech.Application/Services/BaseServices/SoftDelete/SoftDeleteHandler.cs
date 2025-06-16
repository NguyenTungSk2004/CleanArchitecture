using HaiphongTech.SharedKernel.Base;
using HaiphongTech.SharedKernel.Interfaces;
using HaiphongTech.SharedKernel.Specifications;
using MediatR;

namespace HaiphongTech.Application.Services.BaseServices.SoftDelete
{
   public class SoftDeleteHandler<TEntity> : IRequestHandler<SoftDeleteCommand<TEntity>, bool>
    where TEntity : EntityBase, IAuditable, IAggregateRoot
    {
        private readonly IRepository<TEntity> _repository;

        public SoftDeleteHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SoftDeleteCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var spec = new EntitiesByIdsSpecification<TEntity>(request.Ids, false);
            var entities = await _repository.ListAsync(spec, cancellationToken);

            foreach (var entity in entities)
            {
                entity.SoftDelete(request.UserId);
            }

            await _repository.UpdateRangeAsync(entities, cancellationToken);
            return true;
        }
    }

}