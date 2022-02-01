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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public UsuarioRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioListaDTO>> All()
        {
            List<UsuarioListaDTO> lista = new();
            foreach (var item in await Context.Usuarios.ToListAsync())
            {
                lista.Add(new UsuarioListaDTO
                {
                    IdUsuario = item.IdUsuario,
                    ClaveUs = item.ClaveUs,
                    Persona = Mapper.Map<PersonaDTO>(await Context.Personas.SingleOrDefaultAsync(o => o.IdPersona == item.IdPersona)),
                    Sucursal = Mapper.Map<SucursalDTO>(await Context.Sucursals.FirstOrDefaultAsync(s => s.IdSucursal == item.IdSucursal)),
                    Tipo = Mapper.Map<TipoUsuarioDTO>(await Context.TipoUsuarios.FirstOrDefaultAsync(u => u.IdTipo == item.IdTipo))
                });
            }
            return lista;
        }

        public async Task<UsuarioListaDTO> Get(int id)
        {
            var oUsuario = await Context.Usuarios.SingleOrDefaultAsync(o => o.IdUsuario == id);
            if (oUsuario == null)
                throw new CustomException("El usuario no existe");
            var dto = new UsuarioListaDTO
            {
                IdUsuario = oUsuario.IdUsuario,
                ClaveUs = oUsuario.ClaveUs,
                Persona = Mapper.Map<PersonaDTO>(await Context.Personas.SingleOrDefaultAsync(o => o.IdPersona == oUsuario.IdPersona)),
                Sucursal = Mapper.Map<SucursalDTO>(await Context.Sucursals.FirstOrDefaultAsync(s => s.IdSucursal == oUsuario.IdSucursal)),
                Tipo = Mapper.Map<TipoUsuarioDTO>(await Context.TipoUsuarios.FirstOrDefaultAsync(u => u.IdTipo == oUsuario.IdTipo))
            };
            return dto;
        }

        public async Task<UsuarioDTO> Add(UsuarioDTO dto)
        {
            UsuarioValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oUsuario = Mapper.Map<Usuario>(dto);
            Context.Add(oUsuario);
            await Context.SaveChangesAsync();
            return Mapper.Map<UsuarioDTO>(oUsuario);
        }

        public async Task Update(int id, UsuarioDTO dto)
        {
            UsuarioValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oUsuario = await Context.Usuarios.SingleOrDefaultAsync(o => o.IdUsuario == id);
            if (oUsuario == null)
                throw new CustomException("El registro no existe");
            oUsuario.ClaveUs = dto.ClaveUs;
            oUsuario.PasswordUs = dto.PasswordUs;
            oUsuario.IdSucursal = dto.IdSucursal;
            oUsuario.IdTipo = dto.IdTipo;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var oUsuario = await Context.Usuarios.SingleOrDefaultAsync(o => o.IdUsuario == id);
            if (oUsuario == null)
                throw new CustomException("El registro no existe");
            Context.Remove(oUsuario);
            await Context.SaveChangesAsync();
        }
    }
}
