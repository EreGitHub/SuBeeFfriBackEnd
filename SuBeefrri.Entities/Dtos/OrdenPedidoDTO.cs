namespace SuBeefrri.Core.Dtos
{
    public class OrdenPedidoDTO
    {
        public int IdPedido { get; set; }
        public int NroPedido { get; set; }
        public DateTime? Fecha { get; set; } = DateTime.Now;
        public int IdUsuario { get; set; }
        public List<DetallePedidoDTO> DetallePedidos { get; set; }
    }
}
