using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ITokenRepository TokenRepository;
        private readonly IUsuarioRepository UsuarioRepository;
        public TokenController(ITokenRepository tokenRepository, IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            Configuration = configuration;
            TokenRepository = tokenRepository;
            UsuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginDTO dto)
        {
            var oUsuario = await TokenRepository.Login(dto);
            string token = CreateToken(oUsuario);
            var json = new
            {
                token,
                usuario = await UsuarioRepository.Get(oUsuario.IdUsuario)
            };
            return Ok(json);
        }

        private string CreateToken(UsuarioDTO usuario)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, usuario.ClaveUs),
                new Claim(ClaimTypes.Role, usuario.IdTipo.ToString()),
            };

            var Key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:key").Value));

            var cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
