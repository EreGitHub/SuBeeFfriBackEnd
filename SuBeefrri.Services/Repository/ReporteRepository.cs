using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuBeefrri.Contexts.DataContext;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Services.Repository
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public ReporteRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<Reporte1ResponceDTO> Reporte1(DateTime fechaInicio, DateTime fechaFin)
        {
            string _fechaInicio = fechaInicio.ToString("MM/dd/yy");
            string _fechaFin = fechaFin.ToString("MM/dd/yy");
            var query = $"sp_reporte1 '{_fechaInicio}', '{_fechaFin}'";
            var responce = await Context.Set<Report1DTO>().FromSqlRaw(query).ToListAsync();
            var obj = new Reporte1ResponceDTO
            {
                IdSucursal = responce[0].IdSucursal,
                NombreSucursal = responce[0].NombreSucursal,
                Direccion = responce[0].Direccion,
            };
            foreach (var item in responce)
            {
                obj.Items.Add(new Items
                {
                    NombreProducto = item.NombreProducto,
                    Precio_Entrega = item.Precio_Entrega,
                    Precio_Venta = item.Precio_Venta,
                    NombreProveedor = item.NombreProveedor
                });
            }
            return obj;
        }

        public async Task<Reporte2ResponceDTO> Reporte2(DateTime fechaInicio, DateTime fechaFin)
        {
            string _fechaInicio = fechaInicio.ToString("MM/dd/yy");
            string _fechaFin = fechaFin.ToString("MM/dd/yy");
            string query = $"exec sp_ReporteProductosMasVendidos '{_fechaInicio}', '{_fechaFin}'";
            var responce = await Context.Set<Reporte2DTO>().FromSqlRaw(query).ToListAsync();
            var obj = new Reporte2ResponceDTO
            {
                MontoTotal = responce[0].MontoTotal
            };
            foreach (var item in responce)
            {
                obj.Items.Add(new Item
                {
                    NombreProducto = item.NombreProducto,
                    Precio_Entrega = item.Precio_Venta,
                    Precio_Venta = item.Precio_Venta,
                    Proveedor = item.Proveedor,
                    Cantidad = item.Cantidad
                });
            }
            return obj;
        }

    }
}
