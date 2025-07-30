namespace SharedKernel.Interfaces;

public interface ICreationTrackable
{
    long? CreatedBy { get; set; }
    DateTime? CreatedDate { get; set; }
}

public interface IUpdateTrackable
{
    long? UpdatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
}

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    long? DeletedBy { get; set; }
    DateTime? DeletedDate { get; set; }
}
