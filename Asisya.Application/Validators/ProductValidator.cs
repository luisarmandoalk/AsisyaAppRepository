using FluentValidation;
using Asisya.Application.DTOs;

namespace Asisya.Application.Validators
{
    public class ProductValidator : AbstractValidator<CreateProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("La categoría es obligatoria");

            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage("El proveedor es obligatorio");
        }
    }
}