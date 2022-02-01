using FluentValidation;
using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Validators
{
    public class ProductoValidator : AbstractValidator<ProductoDTO>
    {
        public ProductoValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("El nombre no es válido.");
            RuleFor(p => p.PrecioEntrega)
                .GreaterThan(0)
                .WithMessage("Debe especificar el precio de entrega.");
            RuleFor(p => p.PrecioVenta)
                .GreaterThan(0)
                .WithMessage("Debe especificar el precio de venta.");
            RuleFor(p => p.Stock)
                .GreaterThan(0)
                .WithMessage("Debe especificar el stock.");
            RuleFor(p => p.Peso)
                .GreaterThan(0)
                .WithMessage("Debe especificar peso.");
            RuleFor(p => p.IdProveedor)
                .GreaterThan(0)
                .WithMessage("Debe especificar el identificador del proveedor.");
        }
    }
}
