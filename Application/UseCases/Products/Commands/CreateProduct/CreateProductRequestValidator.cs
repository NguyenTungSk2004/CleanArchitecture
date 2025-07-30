using FluentValidation;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductRequestValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductRequestValidator()
        {
        }
    }
}