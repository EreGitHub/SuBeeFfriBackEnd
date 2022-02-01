using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cobros = new HashSet<Cobro>();
            OrderPedidos = new HashSet<OrderPedido>();
        }

        public int IdUsuario { get; set; }
        public string ClaveUs { get; set; } = null!;
        public string? PasswordUs { get; set; }
        public int IdPersona { get; set; }
        public int IdSucursal { get; set; }
        public int IdTipo { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; } = null!;
        public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
        public virtual TipoUsuario IdTipoNavigation { get; set; } = null!;
        public virtual ICollection<Cobro> Cobros { get; set; }
        public virtual ICollection<OrderPedido> OrderPedidos { get; set; }
    }
}
