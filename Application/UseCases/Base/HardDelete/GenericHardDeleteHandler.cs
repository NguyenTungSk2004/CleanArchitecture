using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using SharedKernel.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Base;

namespace Application.UseCases.Base.HardDelete
{
    public abstract class GenericHardDeleteHandler<TEntity, TCommand> : IRequestHandler<TCommand, bool>
        where TEntity : Entity, ISoftDeletable, IAggregateRoot
        where TCommand : GenericHardDeleteCommand
    {
        private readonly IRepository<TEntity> _repository;

        protected GenericHardDeleteHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId != 1)
                    throw new UnauthorizedAccessException("User không có quyền xóa vĩnh viễn bản ghi");

                var spec = new EntitiesByIdsSpecification<TEntity>(request.Ids, true);
                var entities = await _repository.ListAsync(spec, cancellationToken);
                var toDelete = entities.Where(e => (bool)(e.GetType().GetProperty("IsDeleted")?.GetValue(e) ?? false)).ToList();

                if (toDelete == null || toDelete.Count == 0)
                    throw new ExceptionHelper("Không tìm thấy bất kỳ bản ghi nào");

                await _repository.DeleteRangeAsync(toDelete, cancellationToken);
                return true;
            }
            catch (DbUpdateException ex)
            {
                var entityName = typeof(TEntity).Name;
                string errorMessage = ex.InnerException?.Message ?? ex.Message;

                if (errorMessage.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase))
                {
                    var context = MapConstraintToUserFriendlyContext(errorMessage);
                    throw new ExceptionHelper($"Không thể xóa {entityName} vì bản ghi đang được liên kết với {context}.");
                }

                throw new ExceptionHelper($"Đã xảy ra lỗi khi xóa {entityName}.");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (ExceptionHelper ex)
            {
                throw new ExceptionHelper(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Có lỗi khi thực thi: {ex.Message}");
            }
        }
        
        private string MapConstraintToUserFriendlyContext(string sqlMessage)
        {
            if (sqlMessage.Contains("Order", StringComparison.OrdinalIgnoreCase)) return "đơn hàng";
            if (sqlMessage.Contains("Invoice", StringComparison.OrdinalIgnoreCase)) return "hóa đơn";
            if (sqlMessage.Contains("User", StringComparison.OrdinalIgnoreCase)) return "người dùng";

            return "dữ liệu khác";
        }

    }
}