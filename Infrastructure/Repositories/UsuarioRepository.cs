using Microsoft.EntityFrameworkCore;
using NoteTakingAPI.Core.Entities;
using NoteTakingAPI.Core.Interfaces;
using NoteTakingAPI.Infrastructure.Data;

namespace NoteTakingAPI.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetUsuarioAsync(string email, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email 
                                                                && u.Senha == senha);
        }

        public async Task<Boolean> InsertAsync(Usuario usuario)
        {
            var usuarioExiste = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == usuario.Email
                                       && u.Senha == usuario.Senha);

            if (usuarioExiste == null)
            {
                await _context.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
