using System.Text.RegularExpressions;
using HaiphongTech.SharedKernel.Base;
using HaiphongTech.SharedKernel.Exceptions;
using HaiphongTech.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HaiphongTech.Infrastructure.Services;

public abstract class CommonServices<T> : ICommonServices<T>
    where T : class, IAuditable, IAggregateRoot
{
    protected readonly IRepository<T> _repository;

    protected CommonServices(IRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    private string ExtractTableNameFromSqlMessage(string message)
    {
        var match = Regex.Match(message, @"table ""dbo\.(\w+)""", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value : "";
    }

    public virtual async Task<List<T>> SoftDeleteAsync(List<int> ids, int userId)
    {
        try
        {
            var records = await _repository.Table
                .Where(r => ids.Contains(r.Id) && !r.IsDeleted)
                .ToListAsync();

            if (records.Count == 0 || records == null)
                throw new ExceptionHelper("Không tìm thấy bất kỳ bản ghi nào");

            records.ForEach(r =>
            {
                if (r is EntityBase baseEntity)
                    baseEntity.SoftDelete(userId);
            });

            await _repository.UpdateRangeAsync(records);
            return records;
        }
        catch (Exception ex) when (ex is not ExceptionHelper)
        {
            throw new Exception($"Có lỗi khi thực thi: {ex.Message}");
        }
    }

    public virtual async Task<List<T>> HardDeleteAsync(List<int> ids, int userId)
    {
        try
        {
            if (userId != 1)
                throw new UnauthorizedAccessException("User không có quyền xóa vĩnh viễn bản ghi");

            var records = await _repository.Table
                .Where(r => ids.Contains(r.Id) && r.IsDeleted)
                .ToListAsync();

            if (records.Count == 0 || records == null)
                throw new ExceptionHelper("Không tìm thấy bất kỳ bản ghi nào");

            await _repository.DeleteRangeAsync(records);
            return records;
        }
        catch (DbUpdateException ex)
        {
            var entityName = typeof(T).Name;
            var message = ex.InnerException?.Message ?? ex.Message;

            if (message.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase))
            {
                string table = ExtractTableNameFromSqlMessage(message);
                string msg = !string.IsNullOrEmpty(table)
                    ? $"Không thể xóa {entityName} vì đang được liên kết với bảng {table}."
                    : $"Không thể xóa {entityName} vì đang được sử dụng ở nơi khác trong hệ thống.";

                throw new ExceptionHelper(msg);
            }

            throw new ExceptionHelper($"Lỗi khi xóa {entityName}: {message}");
        }
        catch (Exception ex) when (ex is not ExceptionHelper)
        {
            throw new Exception($"Có lỗi khi thực thi: {ex.Message}");
        }
    }

    public virtual async Task<T> RecoveryAsync(int id, int userId)
    {
        try
        {
            if (userId != 1)
                throw new UnauthorizedAccessException("User không có quyền khôi phục bản ghi");

            var record = await _repository.Table
                .FirstOrDefaultAsync(r => r.Id == id) ?? throw new KeyNotFoundException($"Không tìm thấy bản ghi có id: {id}");

            if (!record.IsDeleted)
                throw new ExceptionHelper("Không thể khôi phục bản ghi chưa bị xóa");

            if (record is EntityBase baseEntity) baseEntity.Recover();

            await _repository.UpdateAsync(record);
            return record;
        }
        catch (Exception ex) when (ex is not ExceptionHelper)
        {
            throw new Exception($"Có lỗi khi thực thi: {ex.Message}");
        }
    }
}
