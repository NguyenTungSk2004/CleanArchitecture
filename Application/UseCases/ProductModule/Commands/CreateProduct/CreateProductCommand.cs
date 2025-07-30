using Domain.ProductModule.ValueObjects;
using MediatR;

namespace Application.UseCases.ProductModule.Commands.CreateProduct
{
    public record CreateProductCommand(
        string Code,
        string Name,
        string? BarCode,
        string? TaxRate,
        decimal? CostPrice,
        List<PriceTier> PriceTiers,
        PreOrderInfo PreOrderInfo,
        int? UnitOfQuantityId,
        int? CategoryId,
        int? OriginId,
        int? ManufacturerId
    ) : IRequest<long>;
}
