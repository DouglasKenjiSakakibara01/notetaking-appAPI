using NoteTakingAPI.Core.Entities;

namespace NoteTakingAPI.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetUsuarioAsync(string email, string senha);
        Task<Boolean> InsertAsync(Usuario evento);
        Task<bool> SaveChangesAsync();
    }
}
