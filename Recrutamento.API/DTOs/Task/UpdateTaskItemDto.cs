using Recrutamento.Domain.Enums;

namespace Recrutamento.API.DTOs.Task
{
    public class UpdateTaskItemDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public EnumTaskStatus Status { get; set; }
    }
}
