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
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public TipoUsuarioRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IEnumerable<TipoUsuarioDTO>> All()
        {
            return Mapper.Map<IEnumerable<TipoUsuarioDTO>>(await Context.TipoUsuarios.ToListAsync());
        }

        public async Task<TipoUsuarioDTO> Add(TipoUsuarioDTO dto)
        {
            TipoUsuarioValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oTipoUsuario = Mapper.Map<TipoUsuario>(dto);
            Context.Add(oTipoUsuario);
            await Context.SaveChangesAsync();
            return Mapper.Map<TipoUsuarioDTO>(oTipoUsuario);
        }

        public async Task Update(int id, TipoUsuarioDTO dto)
        {
            TipoUsuarioValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oTipoUsuario = await Context.TipoUsuarios.SingleOrDefaultAsync(o => o.IdTipo == id);
            if (oTipoUsuario == null)
                throw new CustomException("El registro no existe");
            oTipoUsuario.Rol = dto.Rol;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var oTipoUsuario = await Context.TipoUsuarios.SingleOrDefaultAsync(o => o.IdTipo == id);
            if (oTipoUsuario == null)
                throw new CustomException("El registro no existe");
            Context.Remove(oTipoUsuario);
            await Context.SaveChangesAsync();
        }
    }
}
