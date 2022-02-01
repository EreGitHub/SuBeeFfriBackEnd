using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public decimal PrecioEntrega { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public decimal Peso { get; set; }
        public int IdProveedor { get; set; }

        public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
