using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteTakingAPI.Core.Entities
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;
        public DateTime DtCriacao { get; set; }
        public DateTime DtEvento { get; set; }
    }
}
