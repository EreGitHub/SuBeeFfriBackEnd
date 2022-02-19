namespace SuBeefrri.Core.Dtos
{
    public class ProductoListaDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public decimal PrecioEntrega { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public decimal Peso { get; set; }
        public string DireccionFoto { get; set; }
        public ProveedorDTO Proveedor { get; set; }
    }
}
