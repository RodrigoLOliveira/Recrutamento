namespace Recrutamento.API.DTOs.Task
{
    public class CreateTaskItemDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataConclusao { get; set; }
    }
}
