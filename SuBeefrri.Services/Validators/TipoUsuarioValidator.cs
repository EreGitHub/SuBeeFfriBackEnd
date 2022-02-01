using FluentValidation;
using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Validators
{
    public class TipoUsuarioValidator : AbstractValidator<TipoUsuarioDTO>
    {
        public TipoUsuarioValidator()
        {
            RuleFor(t => t.Rol)
                .NotEmpty()
                .WithMessage("El nombre del rol no debe estar vacío");
        }
    }
}
