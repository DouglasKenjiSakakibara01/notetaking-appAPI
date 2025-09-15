using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Core.Interfaces;

namespace NoteTakingAPI.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<EventoService> _logger;

        public UsuarioService(
            IUsuarioRepository usuarioRepository, 
            ILogger<EventoService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<Usuario?> GetUsuarioAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(email, senha);

            if (usuario == null)
            {
                _logger.LogWarning("Usuário não encontrado");
            }

            return usuario;
        }

        public async Task<Boolean> InsertAsync(Usuario usuario)
        {
            var novoUsuario = await _usuarioRepository.InsertAsync(usuario);

            return novoUsuario;
        }

    }
}
