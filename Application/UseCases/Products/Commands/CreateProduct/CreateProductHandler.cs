using Domain.ProductModule.Entities;
using MediatR;
using SharedKernel.Interfaces;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ICurrentUser _currentUser;

        public CreateProductHandler(
            ICurrentUser currentUser,
            IRepository<Product> productRepository
        )
        {
            _currentUser = currentUser;
            _productRepository = productRepository;
        }

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
            if (request.PriceTiers.Count > 0)
            {
                product.UpdatePriceTiers(userId, request.PriceTiers);
            }


            if (request.PreOrderInfo is not null) product.MarkAsPreOrder(userId, request.PreOrderInfo);

            await _productRepository.AddAsync(product, cancellationToken);
            return product.Id;
        }
    }
}
