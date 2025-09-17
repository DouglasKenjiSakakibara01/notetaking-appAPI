using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteTakingAPI.Core.Entities
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        [Key]
        [MaxLength(11)]
        public string Cpf { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateTime DtNascimento { get; set; } = new DateTime();
        [Required]
        public string Senha = string.Empty;




    }
}
