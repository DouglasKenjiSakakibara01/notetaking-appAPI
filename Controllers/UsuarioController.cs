using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteTakingAPI.Core.DTOs;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Services;

namespace NoteTakingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController: ControllerBase
    {
        
        private readonly UsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(UsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
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
