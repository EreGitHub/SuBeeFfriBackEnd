namespace SuBeefrri.Core.Dtos
{
    public class Reporte2ResponceDTO
    {
        public decimal MontoTotal { get; set; }
        public string Mes
        {
            get
            {
                DateTime fechaActual = DateTime.Now;
                return fechaActual.ToString("MMMM");
            }
        }
        public List<Item> Items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public string NombreProducto { get; set; }
        public decimal Precio_Entrega { get; set; }
        public decimal Precio_Venta { get; set; }
        public string Proveedor { get; set; }
        public int Cantidad { get; set; }
    }
}
