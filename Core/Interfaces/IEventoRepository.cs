using NoteTakingAPI.Core.Entities;

namespace NoteTakingAPI.Core.Interfaces
{
    public interface IEventoRepository
    {
        Task<Evento?> GetByIdAsync(int id);
        Task<IEnumerable<Evento>> GetAllAsync();
        Task<Evento> InserOrUpdateAsync(Evento evento);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();

    }
}
