using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Dtos
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
        public decimal PrecioEntrega { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public decimal Peso { get; set; }
        public int IdProveedor { get; set; }        
    }
}
