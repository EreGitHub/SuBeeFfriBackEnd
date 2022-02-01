using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface ISucursalRepository
    {
        Task<SucursalDTO> Add(SucursalDTO dto);
        Task<IEnumerable<SucursalDTO>> All();
        Task Delete(int id);
        Task Update(int id, SucursalDTO dto);
    }
}