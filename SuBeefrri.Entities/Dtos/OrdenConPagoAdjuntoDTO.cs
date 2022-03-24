namespace SuBeefrri.Core.Dtos;
public class OrdenConPagoAdjuntoDTO
{
    public int IdDetallePago { get; set; }
    public string NumeroTransferencia { get; set; } = null!;
    public string? NombreBanco { get; set; }
    public decimal Monto { get; set; }
    public string DireccionFoto { get; set; } = null!;
    public int IdOrderPedido { get; set; }
}
