using Recrutamento.API.DTOs.Task;
using Recrutamento.API.Interfaces;
using Recrutamento.Domain.Enums;
using Recrutamento.Domain.Models;
using Recrutamento.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutamento.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItemDto> CreateTask(CreateTaskItemDto taskDto, string userId)
        {
            var task = new TaskItem
            {
                Titulo = taskDto.Titulo,
                Descricao = taskDto.Descricao,
                DataCriacao = DateTime.UtcNow,
                DataConclusao = taskDto.DataConclusao,
                Status = EnumTaskStatus.Pendente,
                UserId = userId
            };

            if (task.DataConclusao.HasValue && task.DataConclusao < task.DataCriacao)
            {
                throw new ArgumentException("A Data de Conclusão não pode ser anterior à Data de Criação.");
            }

            await _taskRepository.AddAsync(task);

            return new TaskItemDto
            {
                Id = task.Id,
                Titulo = task.Titulo,
                Descricao = task.Descricao,
                DataCriacao = task.DataCriacao,
                DataConclusao = task.DataConclusao,
                Status = task.Status
            };
        }

        public async Task<IEnumerable<TaskItemDto>> GetTasks(string userId)
        {
            var tasks = await _taskRepository.GetTasksByUserAsync(userId);

            return tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataCriacao = t.DataCriacao,
                DataConclusao = t.DataConclusao,
                Status = t.Status
            });
        }

        public async Task<TaskItemDto> GetTaskById(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null || task.UserId != userId)
            {
                return null;
            }

            return new TaskItemDto
            {
                Id = task.Id,
                Titulo = task.Titulo,
                Descricao = task.Descricao,
                DataCriacao = task.DataCriacao,
                DataConclusao = task.DataConclusao,
                Status = task.Status
            };
        }

        public async Task<bool> UpdateTask(int id, UpdateTaskItemDto taskDto, string userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null || task.UserId != userId)
            {
                return false;
            }

            task.Titulo = taskDto.Titulo;
            task.Descricao = taskDto.Descricao;
            task.DataConclusao = taskDto.DataConclusao;
            task.Status = taskDto.Status;

            await _taskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteTask(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null || task.UserId != userId)
            {
                return false;
            }

            await _taskRepository.DeleteAsync(task);
            return true;
        }
    }
}
