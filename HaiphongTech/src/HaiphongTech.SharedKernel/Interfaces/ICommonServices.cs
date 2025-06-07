namespace HaiphongTech.SharedKernel.Interfaces
{
    public interface ICommonServices<T> where T : class, IAuditable
    {
        Task<List<T>> SoftDeleteAsync(List<int> ids, int userId);
        Task<List<T>> HardDeleteAsync(List<int> ids, int userId);
        Task<T> RecoveryAsync(int id, int userId);
    }
}