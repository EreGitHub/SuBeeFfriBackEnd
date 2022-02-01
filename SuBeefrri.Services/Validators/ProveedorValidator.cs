using FluentValidation;
using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Validators
{
    public class ProveedorValidator : AbstractValidator<ProveedorDTO>
    {
        public ProveedorValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("El nombre no es válido.");
        }
    }
}
