using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class OrderPedido
    {
        public OrderPedido()
        {
            DetallePagos = new HashSet<DetallePago>();
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public int IdPedido { get; set; }
        public int NroPedido { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string Estado { get; set; } = null!;

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<DetallePago> DetallePagos { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
