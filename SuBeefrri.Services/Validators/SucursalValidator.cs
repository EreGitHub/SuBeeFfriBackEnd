using FluentValidation;
using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Validators
{
    public class SucursalValidator : AbstractValidator<SucursalDTO>
    {
        public SucursalValidator()
        {
            RuleFor(s => s.Nombre)
                .NotEmpty()
                .WithMessage("El nombre no es válido.");
        }
    }
}
