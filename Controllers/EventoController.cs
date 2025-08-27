using Microsoft.AspNetCore.Mvc;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Core.Interfaces;

namespace NoteTakingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController: ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ILogger<EventoController> _logger;

        public EventoController(IEventoRepository eventoRepository, ILogger<EventoController> logger)
        {
            _eventoRepository = eventoRepository;
            _logger = logger;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetAll()
        {
            try
            {
                var eventos = await _eventoRepository.GetAllAsync();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar eventos");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetById(int id)
        {
            try
            {
                var evento = await _eventoRepository.GetByIdAsync(id);
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
        public async Task<ActionResult<Evento>> InsertOrUpdate(Evento evento)
        {
            try
            {
                if (string.IsNullOrEmpty(evento.Titulo))
                {
                    return BadRequest("Titulo é obrigatorio");
                }

                var retornoEvento = await _eventoRepository.InserOrUpdateAsync(evento);
                return CreatedAtAction(nameof(GetById), new { id = retornoEvento.Id }, retornoEvento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar evento");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _eventoRepository.DeleteAsync(id);
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
