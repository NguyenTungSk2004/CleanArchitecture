using SharedKernel.Interfaces;

namespace SharedKernel.Base;
public abstract class EntityBase : IAuditable
{
    public int Id { get; protected set; }

    public DateTime? CreatedDate { get; protected set; }
    public int? CreatedBy { get; protected set; }

    public DateTime? UpdatedDate { get; protected set; }
    public int? UpdatedBy { get; protected set; }

    public bool IsDeleted { get; protected set; }
    public int? DeletedBy { get; protected set; }
    public DateTime? DeletedDate { get; protected set; }
    public DateTime? RecoveryDate { get; protected set; }

    protected EntityBase()
    {
        IsDeleted = false;
        CreatedDate = null;
        CreatedBy = null;
        UpdatedDate = null;
        UpdatedBy = null;
        DeletedDate = null;
        DeletedBy = null;
        RecoveryDate = null;
    }

    #region Audit Methods
    public void SetCreated(int userId)
    {
        CreatedBy = userId;
        CreatedDate = DateTime.UtcNow;
    }

    public void SetUpdated(int userId)
    {
        UpdatedBy = userId;
        UpdatedDate = DateTime.UtcNow;
    }

    public void SoftDelete(int userId)
    {
        IsDeleted = true;
        DeletedBy = userId;
        DeletedDate = DateTime.UtcNow;
        RecoveryDate = null;
    }

    public void Recover()
    {
        IsDeleted = false;
        RecoveryDate = DateTime.UtcNow;
        DeletedDate = null;
    }

    #endregion
}
