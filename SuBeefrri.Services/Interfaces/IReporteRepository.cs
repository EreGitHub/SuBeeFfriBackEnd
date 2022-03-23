using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IReporteRepository
    {
        Task<Reporte1ResponceDTO> Reporte1(DateTime fechaInicio, DateTime fechaFin);
        Task<Reporte2ResponceDTO> Reporte2(DateTime fechaInicio, DateTime fechaFin);
    }
}
