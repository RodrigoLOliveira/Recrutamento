using Recrutamento.API.DTOs.Task;
using Recrutamento.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recrutamento.API.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItemDto> CreateTask(CreateTaskItemDto taskDto, string userId);
        Task<IEnumerable<TaskItemDto>> GetTasks(string userId);
        Task<TaskItemDto> GetTaskById(int id, string userId);
        Task<bool> UpdateTask(int id, UpdateTaskItemDto taskDto, string userId);
        Task<bool> DeleteTask(int id, string userId);
        Task<IEnumerable<TaskItemDto>> GetTasksByStatus(string userId, EnumTaskStatus status);

    }
}
