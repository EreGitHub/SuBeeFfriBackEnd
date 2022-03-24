namespace SuBeefrri.Core.Dtos
{
    public class ProductosPorUsuarioDTO
    {
        public int IdPerdido { get; set; }
        public int NumeroPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }
        public string? NombreSucursal { get; set; }
        public decimal MontoTotal { get; set; }
        public DetallePagoDTO? DetallePago { get; set; }
        public List<ProductoPorUsuarioDTO>? DetalleProducto { get; set; } = new List<ProductoPorUsuarioDTO>();
    }

    public class DetallePagoDTO
    {
        public string? NumeroTranferencia { get; set; }
        public string? NombreBanco { get; set; }
        public decimal Monto { get; set; }
        public string? DireccionFoto { get; set; }
    }

    public class ProductoPorUsuarioDTO
    {
        public string? NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public int SubTotal { get; set; }
        public string? DireccionFoto { get; set; }
    }
}
