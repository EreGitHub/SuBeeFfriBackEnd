using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdTipo { get; set; }
        public string Rol { get; set; } = null!;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
