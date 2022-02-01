using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IOrdenPedidoRepository
    {
        Task Aprobar(int idPedido);
        Task<IEnumerable<OrdenesListaDTO>> ListarOrdenes();
        Task<OrdenPedidoDTO> Orden(OrdenPedidoDTO dto);
        Task Rechazar(int idPedido);
    }
}