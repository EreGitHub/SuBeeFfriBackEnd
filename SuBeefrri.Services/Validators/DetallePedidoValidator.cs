using FluentValidation;
using SuBeefrri.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuBeefrri.Services.Validators
{
    public class DetallePedidoValidator : AbstractValidator<DetallePedidoDTO>
    {
        public DetallePedidoValidator()
        {
            RuleFor(o => o.Cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayo a cero");
            RuleFor(o => o.IdProducto)
                .GreaterThan(0)
                .WithMessage("El identificador del producto no es válido");
        }
    }
}
