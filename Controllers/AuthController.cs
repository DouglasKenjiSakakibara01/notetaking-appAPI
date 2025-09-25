using Microsoft.AspNetCore.Mvc;
using NoteTakingAPI.Core.DTOs;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Services;

namespace NoteTakingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<UsuarioController> _logger;

        public AuthController(AuthService authService, ILogger<UsuarioController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioLoginDTO usuario)
        {
            try
            {
                var tokenGerado = await _authService.GetUsuarioAsync(usuario.Email, usuario.Senha);

                if (tokenGerado == null) return Unauthorized("Usuário ou senha inválidos");

                return Ok(new UsuarioLoginRetornoDTO(usuario.Email, tokenGerado));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no login");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
