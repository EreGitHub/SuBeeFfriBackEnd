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
    public class ProductoRepository : IProductoRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public ProductoRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IEnumerable<ProductoListaDTO>> All()
        {
            List<ProductoListaDTO> lista = new();
            foreach (var item in await Context.Productos.Where(p => p.Stock > 0).ToListAsync())
            {
                lista.Add(new ProductoListaDTO
                {
                    IdProducto = item.IdProducto,
                    FechaRegistro = item.FechaRegistro,
                    Nombre = item.Nombre,
                    Peso = item.Peso,
                    PrecioEntrega = item.PrecioEntrega,
                    PrecioVenta = item.PrecioVenta,
                    Stock = item.Stock,
                    DireccionFoto = item.DireccionFoto,
                    Proveedor = Mapper.Map<ProveedorDTO>(await Context.Proveedors.SingleOrDefaultAsync(p => p.IdProveedor == item.IdProveedor))
                });
            }
            return lista;
        }

        public async Task<IEnumerable<ProductoDTO>> Buscar(string query)
        {
            if (query.Length < 3)
                throw new CustomException("la cantidad de caracteres debe ser mayor a 3");
            var lst = await Context.Productos.Where(q => q.Nombre.Contains(query) && q.Stock > 0).ToListAsync();
            if (lst.Count > 0)
                return Mapper.Map<IEnumerable<ProductoDTO>>(lst);
            else
                throw new CustomException("No existe el producto");
        }

        public async Task<ProductoDTO> Add(ProductoDTO dto)
        {
            ProductoValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oProducto = Mapper.Map<Producto>(dto);
            Context.Add(oProducto);
            await Context.SaveChangesAsync();
            return Mapper.Map<ProductoDTO>(oProducto);
        }

        public async Task GuardarFoto(int idProducto, string direccionFoto)
        {
            var objProducto = await Context.Productos.FirstOrDefaultAsync(q => q.IdProducto == idProducto);
            if (objProducto == null)
                throw new CustomException("El registro seleccionado no existe");

            objProducto.DireccionFoto = direccionFoto;
            Context.Update(objProducto);
            await Context.SaveChangesAsync();
        }

        public async Task Update(int id, ProductoDTO dto)
        {
            ProductoValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var oProducto = await Context.Productos.SingleOrDefaultAsync(o => o.IdProducto == id);
            if (oProducto == null)
                throw new CustomException("El registro no existe");
            oProducto.Nombre = dto.Nombre;
            oProducto.PrecioEntrega = dto.PrecioEntrega;
            oProducto.PrecioVenta = dto.PrecioVenta;
            oProducto.Stock = dto.Stock;
            oProducto.Peso = dto.Peso;
            oProducto.IdProveedor = dto.IdProveedor;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var oProducto = await Context.Productos.SingleOrDefaultAsync(o => o.IdProducto == id);
            if (oProducto == null)
                throw new CustomException("El registro no existe");
            Context.Remove(oProducto);
            await Context.SaveChangesAsync();
        }
    }
}
