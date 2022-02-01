using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuBeefrri.Contexts.DataContext;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Core.Exceptions;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Services.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly SuBeefrriContext Context;
        private readonly IMapper Mapper;
        public TokenRepository(SuBeefrriContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public async Task<UsuarioDTO> Login(LoginDTO dto)
        {
            var oUsuario = await Context.Usuarios.SingleOrDefaultAsync(o => o.ClaveUs == dto.Password && o.PasswordUs == dto.Password);
            if (oUsuario == null)
                throw new CustomException("El usurio no exite");
            return Mapper.Map<UsuarioDTO>(oUsuario);
        }
    }
}
