using Domain.ProductModule.Rules;
using Domain.ProductModule.ValueObjects;
using SharedKernel.Base;
using SharedKernel.Interfaces;

namespace Domain.ProductModule.Entities;

/// <summary>
/// Hàng hóa
/// </summary>
public class Product : Entity, ISoftDeletable, ICreationTrackable, IUpdateTrackable, IAggregateRoot
{
    public long? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }

    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? BarCode { get; private set; }
    public string? TaxRate { get; private set; }
    public decimal? CostPrice { get; private set; }

    public List<PriceTier> PriceTiers { get; private set; } = new List<PriceTier>();
    public PreOrderInfo PreOrderInfo { get; private set; } = new PreOrderInfo("Từ 7 đến 10 ngày", 0);

    public int? UnitOfQuantityId { get; private set; }
    public int? CategoryId { get; private set; }
    public int? OriginId { get; private set; }
    public int? ManufacturerId { get; private set; }

    #region Constructors
    protected Product() : base(){}

    public Product(
        string code,
        string name,
        decimal? costPrice,
        int? unitOfQuantityId,
        int? categoryId,
        int? originId = null,
        int? manufacturerId = null,
        string? barCode = null,
        string? taxRate = null)
    {
        Code = code;
        Name = name;
        CostPrice = costPrice;
        UnitOfQuantityId = unitOfQuantityId;
        CategoryId = categoryId;
        OriginId = originId;
        ManufacturerId = manufacturerId;
        BarCode = barCode;
        TaxRate = taxRate;
    }
    #endregion

    #region Methods (Domain Behaviors)
    public void UpdateBasicInfo(int updateBy, string name, string? barCode, string? taxRate, decimal? costPrice)
    {
        try
        {
            Name = name;
            BarCode = barCode;
            TaxRate = taxRate;
            CostPrice = costPrice;
            this.MarkUpdated(updateBy);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Cập nhật thông tin cơ bản không thành công.", ex);
        }
    }

    public void UpdatePriceTiers(int updateBy, List<PriceTier> values)
    {
        try
        {
            ProductRules.EnsureValidPriceTiers(values);
            PriceTiers = values;
            this.MarkUpdated(updateBy);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Cập nhật bậc giá không thành công.", ex);
        }
    }

    public void MarkAsPreOrder(int updateBy, PreOrderInfo preOrderInfo)
    {
        try
        {
            PreOrderInfo = preOrderInfo;
            this.MarkUpdated(updateBy);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Đánh dấu hàng hóa là đặt trước không thành công.", ex);
        }
    }

    public void ClearPreOrder(int updateBy)
    {
        try
        {
            PreOrderInfo = new PreOrderInfo("Từ 7 đến 10 ngày", 0);
            this.MarkUpdated(updateBy);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Xóa thông tin đặt trước không thành công.", ex);
        }
    }
    #endregion
}
