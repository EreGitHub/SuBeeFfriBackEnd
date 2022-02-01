using SuBeefrri.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuBeefrri.Core.Dtos
{
    public class OrdenesListaDTO
    {
        public string NombrePersona { get; set; }
        public int IdPedido { get; set; }
        public int NroPedido { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal MontoTotal { get; set; }
        public string EstadoOrden { get; set; }
        public List<ProductoOrdenesDTO> Productos { get; set; } = new List<ProductoOrdenesDTO>();
    }
}
