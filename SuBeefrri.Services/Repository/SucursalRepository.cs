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
    public class SucursalRepository : ISucursalRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public SucursalRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IEnumerable<SucursalDTO>> All()
        {
            return Mapper.Map<IEnumerable<SucursalDTO>>(await Context.Sucursals.ToListAsync());
        }

        public async Task<SucursalDTO> Add(SucursalDTO dto)
        {
            SucursalValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oSucursal = Mapper.Map<Sucursal>(dto);
            Context.Add(oSucursal);
            await Context.SaveChangesAsync();
            return Mapper.Map<SucursalDTO>(oSucursal);
        }

        public async Task Update(int id, SucursalDTO dto)
        {
            SucursalValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oSucursal = await Context.Sucursals.SingleOrDefaultAsync(o => o.IdSucursal == id);
            if (oSucursal == null)
                throw new CustomException("El registro no existe");
            oSucursal.Nombre = dto.Nombre;
            oSucursal.Direccion = dto.Direccion;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var oSucursal = await Context.Sucursals.SingleOrDefaultAsync(o => o.IdSucursal == id);
            if (oSucursal == null)
                throw new CustomException("El registro no existe");
            Context.Remove(oSucursal);
            await Context.SaveChangesAsync();
        }
    }
}
