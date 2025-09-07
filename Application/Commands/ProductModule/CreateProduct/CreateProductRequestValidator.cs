using FluentValidation;

namespace Application.Commands.ProductModule.CreateProduct
{
    public class CreateProductRequestValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductRequestValidator()
        {
        }
    }
}