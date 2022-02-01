namespace SuBeefrri.Core.Dtos
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; } = null!;
        public string? Apellidos { get; set; }
        public string Ci { get; set; } = null!;
        public string? Direccion { get; set; }
    }
}
