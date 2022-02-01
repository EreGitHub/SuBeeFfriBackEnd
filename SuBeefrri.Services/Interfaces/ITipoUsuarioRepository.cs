using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        Task<TipoUsuarioDTO> Add(TipoUsuarioDTO dto);
        Task<IEnumerable<TipoUsuarioDTO>> All();
        Task Delete(int id);
        Task Update(int id, TipoUsuarioDTO dto);
    }
}