namespace SuBeefrri.Api.DTOs
{
    public class AdjuntarComprobantePagoDTO
    {
        public string NumeroTransferencia { get; set; } = null!;
        public string? NombreBanco { get; set; }
        public decimal Monto { get; set; }
        public IFormFile Foto { get; set; }
        public int IdOrderPedido { get; set; }
    }
}
