using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Entities
{
    public partial class Persona
    {
        public Persona()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPersona { get; set; }
        public string Nombres { get; set; } = null!;
        public string? Apellidos { get; set; }
        public string Ci { get; set; } = null!;
        public string? Direccion { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
