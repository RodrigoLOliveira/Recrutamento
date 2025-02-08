using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Recrutamento.Domain.Enums;

namespace Recrutamento.Domain.Models
{
    public class TaskItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O título pode ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataConclusao { get; set; }

        public EnumTaskStatus Status { get; set; } = EnumTaskStatus.Pendente;


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
