using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IPersonaRepository
    {
        Task<PersonaDTO> Add(PersonaDTO dto);
        Task<IEnumerable<PersonaDTO>> All();
        Task Delete(int id);
        Task Update(int id, PersonaDTO dto);
    }
}