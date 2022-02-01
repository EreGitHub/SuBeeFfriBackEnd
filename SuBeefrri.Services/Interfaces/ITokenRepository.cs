using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface ITokenRepository
    {
        Task<UsuarioDTO> Login(LoginDTO dto);
    }
}