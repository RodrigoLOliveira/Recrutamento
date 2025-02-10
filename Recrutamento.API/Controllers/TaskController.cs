using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutamento.API.DTOs.Task;
using Recrutamento.API.Interfaces;
using Recrutamento.API.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recrutamento.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController(ITaskService _taskService) : ControllerBase
    {

        // Criar uma nova tarefa
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskItemDto taskDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            try
            {
                var task = await _taskService.CreateTask(taskDto, userId);
                return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Listar todas as tarefas do usuário autenticado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var tasks = await _taskService.GetTasks(userId);
            return Ok(tasks);
        }

        // Buscar uma tarefa pelo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var task = await _taskService.GetTaskById(id, userId);
            if (task == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            return Ok(task);
        }

        // Atualizar uma tarefa
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskItemDto taskDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var success = await _taskService.UpdateTask(id, taskDto, userId);
            if (!success)
            {
                return NotFound("Tarefa não encontrada.");
            }

            return NoContent();
        }

        // Deletar uma tarefa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var success = await _taskService.DeleteTask(id, userId);
            if (!success)
            {
                return NotFound("Tarefa não encontrada.");
            }

            return NoContent();
        }
    }
}
