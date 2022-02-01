using AutoMapper;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Core.Entities;

namespace SuBeefrri.Services.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PersonaDTO, Persona>().ReverseMap();
            CreateMap<ProductoDTO, Producto>().ReverseMap();
            CreateMap<ProveedorDTO, Proveedor>().ReverseMap();
            CreateMap<SucursalDTO, Sucursal>().ReverseMap();
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<TipoUsuarioDTO, TipoUsuario>().ReverseMap();
            CreateMap<OrdenPedidoDTO, OrderPedido>().ReverseMap();
            CreateMap<DetallePedidoDTO, DetallePedido>().ReverseMap();
        }
    }
}
