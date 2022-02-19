using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SuBeefrri.Contexts.DataContext;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Core.Entities;
using SuBeefrri.Core.Enums;
using SuBeefrri.Core.Exceptions;
using SuBeefrri.Services.HubNotifications;
using SuBeefrri.Services.Interfaces;
using SuBeefrri.Services.Validators;

namespace SuBeefrri.Services.Repository
{
    public class OrdenPedidoRepository : IOrdenPedidoRepository
    {
        private readonly IHubContext<NotificacionHub, INotificacionHubCliente> HubContext;
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        private readonly IMail Mail;
        public OrdenPedidoRepository(
            SuBeefrriContext context,
            IMapper mapper,
            IMail mail,
            IHubContext<NotificacionHub, INotificacionHubCliente> hubContext)
        {
            Context = context;
            Mapper = mapper;
            Mail = mail;
            HubContext = hubContext;
        }
        public async Task<OrdenPedidoDTO> Orden(OrdenPedidoDTO dto)
        {
            OrdenPedidoValidator Validator = new();
            ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                string Message = validationResult.ToString("-");
                throw new CustomException(Message);
            }
            var existeUsuario = Context.Usuarios.SingleOrDefault(o => o.IdUsuario == dto.IdUsuario);
            if (existeUsuario == null)
                throw new CustomException("El usuario proporcinado no existe");
            foreach (var item in dto.DetallePedidos)
            {
                var existeProducto = Context.Productos.SingleOrDefault(o => o.IdProducto == item.IdProducto);
                if (existeProducto == null)
                    throw new CustomException($"EL producto {item.IdProducto} no existe");
                if (existeProducto.Stock < item.Cantidad)
                    throw new CustomException($"Stock insuficiente del producto {item.IdProducto}.");
                existeProducto.Stock -= item.Cantidad;
            }
            var numeroOrden = Context.OrderPedidos.Count() + 1;
            dto.NroPedido = numeroOrden;
            var oOrden = Mapper.Map<OrderPedido>(dto);
            oOrden.Estado = EstadoOrden.Pendiente.ToString();
            Context.Add(oOrden);
            await Context.SaveChangesAsync();
            await HubContext.Clients.All.BroadcastMessage(true);
            await Mail.Send("Nueva orden creada");
            return Mapper.Map<OrdenPedidoDTO>(oOrden);
        }

        public async Task<IEnumerable<OrdenesListaDTO>> ListarOrdenes()
        {
            List<OrdenesListaDTO> lst = new();
            foreach (var itemOrden in await Context.OrderPedidos.ToListAsync())
            {
                var oUsuario = await Context.Usuarios.SingleOrDefaultAsync(o => o.IdUsuario == itemOrden.IdUsuario);
                var oPersona = await Context.Personas.SingleOrDefaultAsync(p => p.IdPersona == oUsuario!.IdPersona);
                var orden = new OrdenesListaDTO
                {
                    NombrePersona = string.Concat(oPersona!.Nombres + " " + oPersona.Apellidos),
                    IdPedido = itemOrden.IdPedido,
                    NroPedido = itemOrden.NroPedido,
                    Fecha = itemOrden.Fecha,
                    EstadoOrden = itemOrden.Estado
                };
                foreach (var itemDetallePedido in await Context.DetallePedidos.Where(o => o.IdPedido == itemOrden.IdPedido).ToArrayAsync())
                {
                    orden.MontoTotal += itemDetallePedido.SubTotal;
                    orden.Productos.Add(new ProductoOrdenesDTO
                    {
                        Cantidad = itemDetallePedido.Cantidad,
                        NombreProducto = (await Context.Productos.SingleOrDefaultAsync(o => o.IdProducto == itemDetallePedido.IdProducto))!.Nombre,
                        SubTotal = itemDetallePedido.SubTotal
                    });
                }
                lst.Add(orden);
            }
            return lst;
        }

        public async Task<List<ProductosPorUsuarioDTO>> ListarOrdenesPorUsuario(int idUsuario)
        {
            List<ProductosPorUsuarioDTO> lst = new List<ProductosPorUsuarioDTO>();
            var oUsuario = await Context.Usuarios.SingleOrDefaultAsync(q => q.IdUsuario == idUsuario);
            var orderPedido = await Context.OrderPedidos.Where(q => q.IdUsuario == idUsuario).ToListAsync();
            var oSucursal = await Context.Sucursals.SingleOrDefaultAsync(q => q.IdSucursal == oUsuario.IdSucursal);
            foreach (var itemOrden in orderPedido)
            {

                var lstDetalle = await Context.DetallePedidos.Where(q => q.IdPedido == itemOrden.IdPedido).ToListAsync();
                foreach (var itemDetalle in lstDetalle)
                {
                    var oProduct = await Context.Productos.SingleOrDefaultAsync(q => q.IdProducto == itemDetalle.IdProducto);
                    lst.Add(new ProductosPorUsuarioDTO
                    {
                        IdPerdido = itemOrden.IdPedido,
                        NombreProducto = oProduct.Nombre,
                        NombreSucursal = oSucursal.Nombre,
                        NumeroPedido = itemOrden.NroPedido,
                        Cantidad = itemDetalle.Cantidad,
                        SubTotal = itemDetalle.SubTotal,
                        Fecha = itemOrden.Fecha,
                        Estado = itemOrden.Estado,
                        DireccionFoto = oProduct.DireccionFoto
                    });
                }
            }
            return lst;
        }

        public async Task Aprobar(int idPedido)
        {
            var oOrden = await Context.OrderPedidos.SingleOrDefaultAsync(o => o.IdPedido == idPedido);
            if (oOrden == null)
                throw new CustomException("El numero de orden proporcionado no existe");
            oOrden!.Estado = EstadoOrden.Aprobado.ToString();
            await Context.SaveChangesAsync();
        }

        public async Task Cobrar(int idPedido, int idUsuarioCobrador)
        {
            var ordenPedidos = await Context.OrderPedidos.Where(q => q.IdPedido == idPedido).FirstOrDefaultAsync();
            ordenPedidos.Estado = EstadoOrden.Cobrado.ToString();
            Context.SaveChanges();
            var DetallePedido = await Context.DetallePedidos.Where(q => q.IdPedido == idPedido).ToListAsync();
            foreach (var item in DetallePedido)
            {
                Entrega entrega = new Entrega();
                entrega.Fecha = DateTime.Now;
                entrega.Estado = 1;
                entrega.IdDetalle = item.IdDetalle;
                Context.Entregas.Add(entrega);
                Context.SaveChanges();
                Cobro cobro = new Cobro();
                cobro.FechaCobro = DateTime.Now;
                cobro.IdUsuario = idUsuarioCobrador;
                cobro.IdEntrega = entrega.IdEntrega;
                Context.Cobros.Add(cobro);
                Context.SaveChanges();
            }
        }

        public async Task Rechazar(int idPedido)
        {
            var oOrden = await Context.OrderPedidos.SingleOrDefaultAsync(o => o.IdPedido == idPedido);
            if (oOrden == null)
                throw new CustomException("El numero de orden proporcionado no existe");
            oOrden!.Estado = EstadoOrden.Rechazado.ToString();
            await Context.SaveChangesAsync();
        }
    }
}
