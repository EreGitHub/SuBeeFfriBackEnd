using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IReporteRepository
    {
        Task<Reporte1ResponceDTO> Reporte1();
        Task<Reporte2ResponceDTO> Reporte2(int numeroMes);
    }
}
