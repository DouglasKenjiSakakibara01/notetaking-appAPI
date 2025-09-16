using Microsoft.AspNetCore.Mvc;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Services;

namespace NoteTakingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController: ControllerBase
    {
        public record UsuarioLoginDTO
        {
            public string Email = string.Empty;
            public string Senha = string.Empty;
        }
        public record UsuarioLoginRetornoDTO
        {
            public string Email = string.Empty;
            public string Token = string.Empty;

            public UsuarioLoginRetornoDTO(string email, string token)
            {
                this.Email = email;
                this.Token = token;
            }
        }
        private readonly UsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(UsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<Usuario>>> Login([FromBody] UsuarioLoginDTO usuario)
        {
            try
            {
                var usuarioExiste = await _usuarioService.GetUsuarioAsync(usuario.Email, usuario.Senha);
                if (usuarioExiste == null) return Unauthorized("Usuário ou senha inválidos");

                var token = _usuarioService.GerarToken(usuario.Email);

                return Ok(new UsuarioLoginRetornoDTO(usuario.Email, token));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Erro no login");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Evento>> Insert([FromBody] Usuario usuario)
        {
            try
            {
                var retornoUsuario = await _usuarioService.InsertAsync(usuario);


                if (!retornoUsuario) return BadRequest();

                return Ok(retornoUsuario);
            }
            catch (Exception ex) 
            {

                _logger.LogError(ex, $"Erro ao inserir usuario");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
