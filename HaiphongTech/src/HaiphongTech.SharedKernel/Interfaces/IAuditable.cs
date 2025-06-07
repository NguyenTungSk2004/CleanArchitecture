namespace HaiphongTech.SharedKernel.Interfaces;

public interface IAuditable
{
    int? CreatedBy { get; }
    int? UpdatedBy { get; }
    int? DeletedBy { get; }

    DateTime? CreatedDate { get; }
    DateTime? UpdatedDate { get; }
    DateTime? DeletedDate { get; }
    DateTime? RecoveryDate { get; }

    bool IsDeleted { get; }
    int Id { get; }
}