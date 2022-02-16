using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IOrdenPedidoRepository
    {
        Task Aprobar(int idPedido);
        Task<IEnumerable<OrdenesListaDTO>> ListarOrdenes();
        Task<List<ProductosPorUsuarioDTO>> ListarOrdenesPorUsuario(int idUsuario);
        Task<OrdenPedidoDTO> Orden(OrdenPedidoDTO dto);
        Task Cobrar(int idPedido, int idUsuarioCobrador);
        Task Rechazar(int idPedido);
    }
}