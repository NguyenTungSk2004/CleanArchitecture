namespace HaiphongTech.Application.Services.Products.Commands.CreateProduct;

using FluentValidation;
using MediatR;
using Base.Application.Service.Core.ValueObjects;

public record CreateProductCommand(
    string Code = "",
    string Name = "",
    string? BarCode = null,
    string? TaxRate = null,
    decimal? CostPrice = null,
    PriceTier? PriceTier1 = null,
    PriceTier? PriceTier2 = null,
    PriceTier? PriceTier3 = null,
    PriceTier? PriceTier4 = null,
    PreOrderInfo? PreOrderInfo = null,
    int? UnitOfQuantityId = null,
    int? CategoryId = null,
    int? OriginId = null,
    int? ManufacturerId = null
) : IRequest<int>;

// public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
// {
//     public CreateProductCommandValidator()
//     {
//         RuleFor(x => x.Code)
//             .NotEmpty().WithMessage("Product code is required")
//             .MaximumLength(50);

//         RuleFor(x => x.Name)
//             .NotEmpty().WithMessage("Product name is required")
//             .MaximumLength(100);

//         RuleFor(x => x.CostPrice)
//             .GreaterThanOrEqualTo(0).When(x => x.CostPrice.HasValue)
//             .WithMessage("Cost price must be non-negative");

//         RuleFor(x => x.UnitOfQuantityId)
//             .NotNull().WithMessage("Unit of quantity is required");

//         RuleFor(x => x.CategoryId)
//             .NotNull().WithMessage("Category is required");

//         RuleFor(x => x.BarCode)
//             .MaximumLength(100)
//             .When(x => !string.IsNullOrEmpty(x.BarCode));

//         RuleFor(x => x.TaxRate)
//             .MaximumLength(20)
//             .When(x => !string.IsNullOrEmpty(x.TaxRate));
//     }
// }