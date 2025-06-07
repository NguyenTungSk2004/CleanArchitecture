using Base.Application.Service.Core.ValueObjects;
using HaiphongTech.Application.Services.Products.Commands.CreateProduct;

namespace HaiphongTech.API.Requests
{
    public class CreateProductRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? BarCode { get; set; }
        public string? TaxRate { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? SellingPrice1 { get; set; }
        public int? Quantity1 { get; set; }
        public decimal? SellingPrice2 { get; set; }
        public int? Quantity2 { get; set; }
        public decimal? SellingPrice3 { get; set; }
        public int? Quantity3 { get; set; }
        public decimal? SellingPrice4 { get; set; }
        public int? Quantity4 { get; set; }
        public int? UnitOfQuantityId { get; set; }
        public int? CategoryId { get; set; }
        public int? OriginId { get; set; }
        public int? ManufacturerId { get; set; }
        public string? WaitingTimeUntilAvailable { get; set; }
        public int PreOrderQuantity { get; set; }

        public CreateProductCommand ToCommand()
        {
            return new CreateProductCommand(
                Code,
                Name,
                BarCode,
                TaxRate,
                CostPrice,
                PriceTier1: SellingPrice1.HasValue && Quantity1.HasValue ? new PriceTier(SellingPrice1.Value, Quantity1.Value) : null,
                PriceTier2: SellingPrice2.HasValue && Quantity2.HasValue ? new PriceTier(SellingPrice2.Value, Quantity2.Value) : null,
                PriceTier3: SellingPrice3.HasValue && Quantity3.HasValue ? new PriceTier(SellingPrice3.Value, Quantity3.Value) : null,
                PriceTier4: SellingPrice4.HasValue && Quantity4.HasValue ? new PriceTier(SellingPrice4.Value, Quantity4.Value) : null,
                PreOrderInfo: new PreOrderInfo(WaitingTimeUntilAvailable, PreOrderQuantity),
                UnitOfQuantityId,
                CategoryId,
                OriginId,
                ManufacturerId
            );
        }
    }
}