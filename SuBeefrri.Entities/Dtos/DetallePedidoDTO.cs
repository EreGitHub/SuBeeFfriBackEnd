using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuBeefrri.Core.Dtos
{
    public class DetallePedidoDTO
    {        
        public int IdDetalle { get; set; }
        public int Cantidad { get; set; }
        public int SubTotal { get; set; }
        public int IdProducto { get; set; }
        public int IdPedido { get; set; }
    }
}
