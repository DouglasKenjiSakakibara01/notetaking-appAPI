using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Core.Interfaces;
using NoteTakingAPI.Services;

namespace NoteTakingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventoController: ControllerBase
    {
        private readonly EventoService _eventoService;
        private readonly ILogger<EventoController> _logger;

        public EventoController(EventoService eventoService, ILogger<EventoController> logger)
        {
            _eventoService = eventoService;
            _logger = logger;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetAll()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventos();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar eventos");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetById([FromRoute] int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id);
                if (evento == null) return NotFound();
                return Ok(evento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar evento {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> InsertOrUpdate([FromBody] Evento evento)
        {
            try
            {
                if (string.IsNullOrEmpty(evento.Titulo))
                {
                    return BadRequest("Titulo é obrigatorio");
                }

                var retornoEvento = await _eventoService.InsertOrUpdateEvento(evento);
                return CreatedAtAction(nameof(GetById), new { id = retornoEvento.Id }, retornoEvento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar evento");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _eventoService.DeleteEvento(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar evento {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }

}
