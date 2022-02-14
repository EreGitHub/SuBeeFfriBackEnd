using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SuBeefrri.Contexts.DataContext;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Core.Entities;
using SuBeefrri.Core.Exceptions;
using SuBeefrri.Services.Interfaces;
using SuBeefrri.Services.Validators;

namespace SuBeefrri.Services.Repository
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public PersonaRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IEnumerable<PersonaDTO>> All()
        {
            var lst = await Context.Personas.ToListAsync();
            return Mapper.Map<IEnumerable<PersonaDTO>>(lst);
        }

        public async Task<PersonaDTO> Add(PersonaDTO dto)
        {
            PersonaValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oPersona = Mapper.Map<Persona>(dto);
            Context.Add(oPersona);
            await Context.SaveChangesAsync();
            return Mapper.Map<PersonaDTO>(oPersona);
        }

        public async Task Update(int id, PersonaDTO dto)
        {

            PersonaValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oPersona = await Context.Personas.SingleOrDefaultAsync(o => o.IdPersona == id);
            if (oPersona == null)
                throw new CustomException("El registro no existe");
            oPersona.Nombres = dto.Nombres;
            oPersona.Apellidos = dto.Apellidos;
            oPersona.Ci = dto.Ci;
            oPersona.Direccion = dto.Direccion;
            oPersona.DireccionFoto = dto.DireccionFoto;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var oPersona = await Context.Personas.SingleOrDefaultAsync(o => o.IdPersona == id);
            if (oPersona == null)
                throw new CustomException("El registro no existe");
            Context.Remove(oPersona);
            await Context.SaveChangesAsync();
        }
    }
}
