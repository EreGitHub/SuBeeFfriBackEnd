using FluentValidation;
using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(p => p.ClaveUs)
                .NotEmpty()
                .WithMessage("Debe proporcianar el nombre del usuario.");
            RuleFor(p => p.PasswordUs)
                .NotEmpty()
                .WithMessage("Debe proporcianar una contraseña.");
            RuleFor(u => u.IdPersona)
                .GreaterThan(0)
                .WithMessage("El Identificador de la persona no es válido.");
            RuleFor(u => u.IdSucursal)
                .GreaterThan(0)
                .WithMessage("El identificador del usuario debe ser válido.");
            RuleFor(u => u.IdTipo)
                .GreaterThan(0)
                .WithMessage("El identificador del rol no es válido.");
        }
    }
}
