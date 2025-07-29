using SharedKernel.Base;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using MediatR;

namespace GateWay.Application.Services.BaseServices.Recovery
{
    public class RecoveryHandler<TEntity>: IRequestHandler<RecoveryCommand<TEntity>, bool>
        where TEntity : EntityBase, IAuditable, IAggregateRoot
    {
        private readonly IRepository<TEntity> _repository;
        public RecoveryHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(RecoveryCommand<TEntity> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId != 1)
                    throw new UnauthorizedAccessException("User không có quyền khôi phục bản ghi");

                var record = await _repository.GetByIdAsync(
                    request.Id,
                    cancellationToken
                )
                    ?? throw new KeyNotFoundException($"Không tìm thấy bản ghi có id: {request.Id}");

                if (!record.IsDeleted)
                    throw new ExceptionHelper("Không thể khôi phục bản ghi chưa bị xóa");

                if (record is EntityBase entity) entity.Recover();
                else return false;

                await _repository.UpdateAsync(record, cancellationToken);
                return true;
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new Exception($"Không tìm thấy bản ghi: {knfEx.Message}", knfEx);
            }
            catch (UnauthorizedAccessException uaeEx)
            {
                throw new Exception($"Quyền truy cập bị từ chối: {uaeEx.Message}", uaeEx);
            }
            catch (ExceptionHelper ehEx)
            {
                throw new ExceptionHelper($"Lỗi trong quá trình khôi phục: {ehEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi khôi phục bản ghi: {ex.Message}", ex);
            }
        }
    }
}