namespace SuBeefrri.Core.Dtos
{
    public class OrdenesListaDTO
    {
        public string? NombrePersona { get; set; }
        public int IdPedido { get; set; }
        public int NroPedido { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal MontoTotal { get; set; }
        public string? EstadoOrden { get; set; }
        public DetallePagoDTO? DetallePago { get; set; }
        public List<ProductoOrdenesDTO> Productos { get; set; } = new List<ProductoOrdenesDTO>();
    }
}
