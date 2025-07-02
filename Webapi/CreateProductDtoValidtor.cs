using FluentValidation;

using Webapi.DTOs;

namespace Webapi
{
    public class CreateProductDtoValidtor : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidtor()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.Description).NotNull().NotEmpty();
            RuleFor(p => p.Price).NotNull().NotEmpty().GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(p => p.PictureUrl).NotNull().NotEmpty();
            RuleFor(p => p.Type).NotNull().NotEmpty();
            RuleFor(p => p.Brand).NotNull().NotEmpty();
            RuleFor(p => p.QuantityInStock).NotNull().NotEmpty().GreaterThan(0).WithMessage("Quantity must be greater than zero");
        }
    }
}
