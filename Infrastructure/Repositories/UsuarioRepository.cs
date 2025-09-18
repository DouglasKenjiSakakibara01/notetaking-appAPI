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
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null) return usuario;

            bool senhaExiste = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

            return senhaExiste ? usuario : null;
        }

        public async Task<Boolean> InsertAsync(Usuario usuario)
        {
            var usuarioExiste = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == usuario.Email);

            if (usuarioExiste == null)
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
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
