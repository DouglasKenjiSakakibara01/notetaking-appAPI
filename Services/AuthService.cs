using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Core.Interfaces;

namespace NoteTakingAPI.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<EventoService> _logger;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            ILogger<EventoService> logger,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string?> GetUsuarioAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(email, senha);

            if (usuario == null)
            {
                _logger.LogWarning("Usuário não encontrado");
                return null;
            }

            return GerarToken(usuario.Email);
        }

        public string GerarToken(string email)
        {
            var key = _configuration["Jwt:Key"];
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email)
            };

            var credenciais = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
