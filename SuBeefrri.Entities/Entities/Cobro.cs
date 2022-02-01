using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class Cobro
    {
        public int IdCobro { get; set; }
        public DateTime FechaCobro { get; set; }
        public int? IdUsuario { get; set; }
        public int IdEntrega { get; set; }

        public virtual Entrega IdEntregaNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
