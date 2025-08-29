using NoteTakingAPI.Core.Interfaces;
using NoteTakingAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NoteTakingAPI.Core.Entities;


namespace NoteTakingAPI.Infrastructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Evento?> GetByIdAsync(int id)
        {
            return await _context.Eventos
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Evento>> GetAllAsync()
        {
            return await _context.Eventos
                .ToListAsync();
        }

        public async Task<Evento> InserOrUpdateAsync(Evento evento)
        {
            var eventoExistente = await _context.Eventos
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == evento.Id);

            if (eventoExistente == null) { 
                evento.DtCriacao = DateTime.UtcNow;
                await _context.Eventos.AddAsync(evento);
            }
            else
            {
                evento.DtCriacao = eventoExistente.DtCriacao;
                _context.Eventos.Update(evento);
            }
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<Boolean> DeleteAsync(int id)
        {
            var evento = await GetByIdAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
