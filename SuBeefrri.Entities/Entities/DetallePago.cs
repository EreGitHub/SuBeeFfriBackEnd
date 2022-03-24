using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class DetallePago
    {
        public int IdDetallePago { get; set; }
        public string? NumeroTransferencia { get; set; }
        public string? NombreBanco { get; set; }
        public decimal? Monto { get; set; }
        public string? DireccionFoto { get; set; }
        public int IdOrderPedido { get; set; }

        public virtual OrderPedido IdOrderPedidoNavigation { get; set; } = null!;
    }
}
