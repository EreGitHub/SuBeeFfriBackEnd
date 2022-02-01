using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdSucursal { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Direccion { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
