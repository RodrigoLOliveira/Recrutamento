using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrutamento.API.DTOs.Task;
using Recrutamento.Domain.Enums;
using Recrutamento.Domain.Models;
using Recrutamento.Infra;
using System;
using System.Security.Claims;

namespace Recrutamento.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController(RecrutamentoDbContext _context, 
                                UserManager<User> _userManager)
                                : ControllerBase
    {

        // Criar uma nova tarefa
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskItemDto taskDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);

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
                return BadRequest("A Data de Conclusão não pode ser anterior à Data de Criação.");
            }

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, new TaskItemDto
            {
                Id = task.Id,
                Titulo = task.Titulo,
                Descricao = task.Descricao,
                DataCriacao = task.DataCriacao,
                DataConclusao = task.DataConclusao,
                Status = task.Status
            });
        }

        // Listar todas as tarefas do usuário autenticado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tasks = await _context.TaskItems
                .Where(t => t.UserId == userId)
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    DataCriacao = t.DataCriacao,
                    DataConclusao = t.DataConclusao,
                    Status = t.Status
                })
                .ToListAsync();

            return Ok(tasks);
        }

        // Buscar uma tarefa pelo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await _context.TaskItems
                .Where(t => t.Id == id && t.UserId == userId)
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    DataCriacao = t.DataCriacao,
                    DataConclusao = t.DataConclusao,
                    Status = t.Status
                })
                .FirstOrDefaultAsync();

            if (task == null) return NotFound();
            return Ok(task);
        }

        // Atualizar uma tarefa
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskItemDto taskDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null) return NotFound();

            task.Titulo = taskDto.Titulo;
            task.Descricao = taskDto.Descricao;
            task.DataConclusao = taskDto.DataConclusao;
            task.Status = taskDto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Deletar uma tarefa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null) return NotFound();

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
