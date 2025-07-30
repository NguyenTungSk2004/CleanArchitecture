using FluentValidation;

namespace Application.UseCases.ProductModule.Commands.CreateProduct
{
    public class CreateProductRequestValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductRequestValidator()
        {
        }
    }
}