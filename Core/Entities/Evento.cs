using System.ComponentModel.DataAnnotations;

namespace NoteTakingAPI.Core.Entities
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;
        public DateTime DtCriacao { get; set; }
    }
}
