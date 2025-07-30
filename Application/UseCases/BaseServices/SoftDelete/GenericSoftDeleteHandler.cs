using SharedKernel.Base;
using SharedKernel.Interfaces;
using SharedKernel.Specifications;
using MediatR;

namespace Application.UseCases.BaseServices.SoftDelete
{
   public abstract class GenericSoftDeleteHandler<TEntity, TCommand> : IRequestHandler<TCommand, bool>
        where TEntity : EntityBase, IAuditable, IAggregateRoot
        where TCommand : GenericSoftDeleteCommand
    {
        private readonly IRepository<TEntity> _repository;

        protected GenericSoftDeleteHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(TCommand request, CancellationToken cancellationToken)
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