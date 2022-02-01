using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IProveedorRepository
    {
        Task<ProveedorDTO> Add(ProveedorDTO dto);
        Task<IEnumerable<ProveedorDTO>> All();
        Task Delete(int id);
        Task Update(int id, ProveedorDTO dto);
    }
}