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

        public async Task<Reporte1ResponceDTO> Reporte1()
        {
            var responce = Context.Set<Report1DTO>().FromSqlRaw("sp_reporte1").ToList();
            var obj = new Reporte1ResponceDTO
            {
                IdSucursal = responce[0].IdSucursal,
                NombreSucursal = responce[0].NombreSucursal,
                Direccion=responce[0].Direccion,
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

    }
}
