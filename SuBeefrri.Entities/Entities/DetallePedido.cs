using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class DetallePedido
    {
        public DetallePedido()
        {
            Entregas = new HashSet<Entrega>();
        }

        public int IdDetalle { get; set; }
        public int Cantidad { get; set; }
        public int SubTotal { get; set; }
        public int IdProducto { get; set; }
        public int IdPedido { get; set; }

        public virtual OrderPedido IdPedidoNavigation { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual ICollection<Entrega> Entregas { get; set; }
    }
}
