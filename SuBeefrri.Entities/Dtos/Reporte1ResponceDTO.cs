namespace SuBeefrri.Core.Dtos
{
    public class Reporte1ResponceDTO
    {
        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
        public List<Items> Items { get; set; } = new List<Items>();
    }

    public class Items
    {
        public string NombreProducto { get; set; }
        public decimal Precio_Entrega { get; set; }
        public decimal Precio_Venta { get; set; }
        public string NombreProveedor { get; set; }
    }
}
