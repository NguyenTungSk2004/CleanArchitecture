namespace Application.Services.Products.Commands.CreateProduct;

using MediatR;
using Domain.Repositories;
using Domain.Entities.Products.ProductAggregate;
using SharedKernel.Interfaces;
using Base.Application.Service.Core.ValueObjects;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUser _currentUser;

    public CreateProductHandler(
        ICurrentUser currentUser,
        IProductRepository productRepository
    )
    {
        _currentUser = currentUser;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (_currentUser.UserId == null)
        {
            // throw new UnauthorizedAccessException("User is not authenticated.");
        }
        int userId = _currentUser.UserId ?? 0; // For testing purposes, set a default user ID

        var product = new Product(
            request.Code,
            request.Name,
            request.CostPrice,
            request.UnitOfQuantityId,
            request.CategoryId,
            request.OriginId,
            request.ManufacturerId,
            request.BarCode,
            request.TaxRate
        );

        PriceTier?[] priceTiers = new PriceTier?[4]
        {
            request.PriceTier1,
            request.PriceTier2,
            request.PriceTier3,
            request.PriceTier4
        };
        foreach (var (priceTier, index) in priceTiers.Select((pt, idx) => (pt, idx)))
        {
            if (priceTier is not null)
            {
                product.UpdatePriceTier(userId, index + 1, priceTier);
            }
        }

        if (request.PreOrderInfo is not null) product.MarkAsPreOrder(userId, request.PreOrderInfo);

        await _productRepository.AddAsync(product, cancellationToken);
        return product.Id;
    }
}
