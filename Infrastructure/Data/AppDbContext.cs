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
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura como as entidades mapeiam para tabelas
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Titulo)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Descricao)
                    .HasMaxLength(500);
                entity.Property(p => p.DtCriacao);
                entity.Property(p => p.DtEvento);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasKey(u => u.Email);
                entity.Property(u => u.Senha);
                entity.Property(u => u.DtNascimento);
                entity.Property(u => u.Cpf)
                      .HasMaxLength(11);
            });
        }
    }
}
