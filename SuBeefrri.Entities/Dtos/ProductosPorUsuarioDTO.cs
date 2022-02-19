namespace SuBeefrri.Core.Dtos
{
    public class ProductosPorUsuarioDTO
    {
        public int IdPerdido { get; set; }
        public string NombreProducto { get; set; }
        public string NombreSucursal { get; set; }
        public int NumeroPedido { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int SubTotal { get; set; }
        public string Estado { get; set; }
        public string DireccionFoto { get; set; }
    }
}
