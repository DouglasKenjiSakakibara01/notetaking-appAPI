using Microsoft.EntityFrameworkCore;
using NoteTakingAPI.Core.Entities;

namespace NoteTakingAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as configurações
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet representa uma tabela no banco
        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura como as entidades mapeiam para tabelas
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.DtCriacao).IsRequired();
                entity.Property(p => p.Titulo).IsRequired();
            });
        }
    }
}
