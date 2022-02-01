using FluentValidation;
using SuBeefrri.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuBeefrri.Services.Validators
{
    public class OrdenPedidoValidator : AbstractValidator<OrdenPedidoDTO>
    {
        public OrdenPedidoValidator()
        {
            RuleFor(o => o.IdUsuario)
                .NotEmpty()
                .WithMessage("debe proporcinar el identificador del cliente");
            RuleFor(o => o.DetallePedidos)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Deber especificar los productos de la orden")
                .NotEmpty()
                .WithMessage("Debe especificar al menos un producto de la orden");
            RuleForEach(o => o.DetallePedidos)
                .SetValidator(new DetallePedidoValidator());
        }
    }
}
