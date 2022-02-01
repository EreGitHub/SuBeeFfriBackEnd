using System;
using System.Collections.Generic;

namespace SuBeefrri.Core.Dtos
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string ClaveUs { get; set; } = null!;
        public string? PasswordUs { get; set; }
        public int IdPersona { get; set; }
        public int IdSucursal { get; set; }
        public int IdTipo { get; set; }
    }
}
