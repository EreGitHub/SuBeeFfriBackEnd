using FluentValidation;
using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Validators
{
    public class PersonaValidator : AbstractValidator<PersonaDTO>
    {
        public PersonaValidator()
        {
            RuleFor(p => p.Nombres)
                .NotEmpty()
                .WithMessage("El nombre no debe estar vacío.");
            RuleFor(p => p.Ci)
                 .NotEmpty()
                 .WithMessage("El ci del cliente es obligatorio");
        }
    }
}
