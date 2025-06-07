namespace HaiphongTech.Application.Services.Products.Commands.CreateProduct;

using MediatR;
using HaiphongTech.Domain.Repositories;
using HaiphongTech.Domain.Entities.Products.ProductAggregate;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            code: request.Sku,
            name: request.Name,
            costPrice: null, // Assuming cost price is not provided in the command
            unitOfQuantityId: null, // Assuming unit of quantity is not provided in the command
            categoryId: null, // Assuming category ID is not provided in the command
            barCode: null, // Assuming bar code is not provided in the command
            taxRate: null // Assuming tax rate is not provided in the command
        );

        await _productRepository.AddAsync(product, cancellationToken);
        return product.Id;
    }
}
