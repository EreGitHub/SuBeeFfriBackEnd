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
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public ProveedorRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IEnumerable<ProveedorDTO>> All()
        {
            return Mapper.Map<IEnumerable<ProveedorDTO>>(await Context.Proveedors.ToListAsync());
        }

        public async Task<ProveedorDTO> Add(ProveedorDTO dto)
        {
            ProveedorValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oProveedor = Mapper.Map<Proveedor>(dto);
            Context.Add(oProveedor);
            await Context.SaveChangesAsync();
            return Mapper.Map<ProveedorDTO>(oProveedor);
        }

        public async Task Update(int id, ProveedorDTO dto)
        {
            ProveedorValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oProveedor = await Context.Proveedors.SingleOrDefaultAsync(o => o.IdProveedor == id);
            if (oProveedor == null)
                throw new CustomException("El registro no existe");
            oProveedor.Nombre = dto.Nombre;
            oProveedor.Nit = dto.Nit;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var oProveedor = await Context.Proveedors.SingleOrDefaultAsync(o => o.IdProveedor == id);
            if (oProveedor == null)
                throw new CustomException("El registro no existe");
            Context.Remove(oProveedor);
            await Context.SaveChangesAsync();
        }
    }
}
