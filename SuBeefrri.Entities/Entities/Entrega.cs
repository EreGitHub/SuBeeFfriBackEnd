using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class Entrega
    {
        public Entrega()
        {
            Cobros = new HashSet<Cobro>();
        }

        public int IdEntrega { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        public int IdDetalle { get; set; }

        public virtual DetallePedido IdDetalleNavigation { get; set; } = null!;
        public virtual ICollection<Cobro> Cobros { get; set; }
    }
}
