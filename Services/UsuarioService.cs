using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace NoteTakingAPI.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<EventoService> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioService(
            IUsuarioRepository usuarioRepository, 
            ILogger<EventoService> logger,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _configuration = configuration;
        }


        public async Task<Boolean> InsertAsync(Usuario usuario)
        {
            var novoUsuario = await _usuarioRepository.InsertAsync(usuario);

            return novoUsuario;
        }        

    }
}
