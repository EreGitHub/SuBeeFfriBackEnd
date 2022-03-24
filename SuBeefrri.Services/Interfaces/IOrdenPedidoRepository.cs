using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IOrdenPedidoRepository
    {
        Task Enviada(int idPedido);
        Task EnviarOrdenPagada(int idPedido, int idUsuarioCobrador);
        Task<IEnumerable<OrdenesListaDTO>> ListarOrdenes();
        Task<List<ProductosPorUsuarioDTO>> ListarOrdenesPorUsuario(int idUsuario);
        Task<OrdenPedidoDTO> OrdenarSinPago(OrdenPedidoDTO dto);
        Task Cobrar(int idPedido, int idUsuarioCobrador);
        Task<OrdenPedidoDTO> OrdenConPago(OrdenPedidoDTO orden);
        Task AdjuntarOrdenPago(OrdenConPagoAdjuntoDTO orden);
    }
}