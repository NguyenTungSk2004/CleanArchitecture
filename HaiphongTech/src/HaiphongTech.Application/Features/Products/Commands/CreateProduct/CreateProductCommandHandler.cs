namespace HaiphongTech.Application.Features.Products.Commands.CreateProduct;

using MediatR;
using HaiphongTech.Domain.Entities;
using HaiphongTech.Domain.Repositories;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Sku, request.Price);

        await _productRepository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}
