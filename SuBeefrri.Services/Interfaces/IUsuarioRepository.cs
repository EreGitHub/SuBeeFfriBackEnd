using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioDTO> Add(UsuarioDTO dto);
        Task<IEnumerable<UsuarioListaDTO>> All();
        Task Delete(int id);
        Task<UsuarioListaDTO> Get(int id);
        Task Update(int id, UsuarioDTO dto);
    }
}