using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Core.Interfaces;
using NoteTakingAPI.Infrastructure.Data;

namespace NoteTakingAPI.Services
{
    public class EventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ILogger<EventoService> _logger;

        public EventoService(
            IEventoRepository eventoRepository,
            AppDbContext context,
            ILogger<EventoService> logger)
        {
            _eventoRepository = eventoRepository;
            _logger = logger;
        }

        public async Task<Evento?> GetEventoByIdAsync(int id)
        {
            
            var evento = await _eventoRepository.GetByIdAsync(id);

            if (evento == null)
            {
                _logger.LogWarning("Evento não encontrado");
            }

            return evento;
            
        }
        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
            var eventos = await _eventoRepository.GetAllAsync();

            if (eventos == null) 
            {
                _logger.LogWarning("Não foi encontrado nenhum registro de Evento");
            }

            return eventos;

        }

        public async Task<Evento> InsertOrUpdateEvento(Evento evento)
        {
            var novoEvento = await _eventoRepository.InserOrUpdateAsync(evento);
            return novoEvento;
            
        }

        public async Task<Boolean> DeleteEvento(int id)
        {
            var evento = await _eventoRepository.DeleteAsync(id);

            if (evento)
            {
                return true;
            }
                
            return false;
           
        }
    }
}
