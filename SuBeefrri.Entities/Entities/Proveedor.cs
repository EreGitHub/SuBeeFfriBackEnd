using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Nit { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
