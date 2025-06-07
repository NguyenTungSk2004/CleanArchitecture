using Base.Application.Service.Core.ValueObjects;
using HaiphongTech.SharedKernel.Base;
using HaiphongTech.SharedKernel.Interfaces;

namespace HaiphongTech.Domain.Entities.Products.ProductAggregate;

/// <summary>
/// Hàng hóa
/// </summary>
public class Product : EntityBase, IAggregateRoot
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? BarCode { get; private set; }
    public string? TaxRate { get; private set; }
    public decimal? CostPrice { get; private set; }

    public PriceTier? PriceTier1 { get; private set; }
    public PriceTier? PriceTier2 { get; private set; }
    public PriceTier? PriceTier3 { get; private set; }
    public PriceTier? PriceTier4 { get; private set; }

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
            SetUpdated(updateBy);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Cập nhật thông tin cơ bản không thành công.", ex);
        }
    }

    public void UpdatePriceTier(int updateBy, int tierIndex, PriceTier value)
    {
        try
        {
            switch (tierIndex)
            {
                case 1: PriceTier1 = value; break;
                case 2: PriceTier2 = value; break;
                case 3: PriceTier3 = value; break;
                case 4: PriceTier4 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(tierIndex), "Chỉ số bậc giá không hợp lệ.");
            }
            SetUpdated(updateBy);
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
            SetUpdated(updateBy);
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
            SetUpdated(updateBy);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Xóa thông tin đặt trước không thành công.", ex);
        }
    }
    #endregion
}
