namespace SuBeefrri.Core.Dtos
{
    public class UsuarioListaDTO
    {
        public int IdUsuario { get; set; }
        public string ClaveUs { get; set; } = null!;
        public PersonaDTO Persona { get; set; }
        public SucursalDTO Sucursal { get; set; }
        public TipoUsuarioDTO Tipo { get; set; }
    }
}
