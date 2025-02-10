using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Recrutamento.Wpf.DTOs;
using System.Text;
using Recrutamento.Domain.Enums;

namespace Recrutamento.Wpf.Services
{
    public class TaskService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7058/")
        };

        public async Task<List<TaskItemDto>> GetTasksAsync()
        {
            if (string.IsNullOrEmpty(AuthService.Token))
                throw new UnauthorizedAccessException("Usuário não autenticado!");

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthService.Token);
                var response = await _httpClient.GetAsync("api/task");

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Erro ao carregar tarefas!");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                return JsonSerializer.Deserialize<List<TaskItemDto>>(jsonResponse, opt) ?? new List<TaskItemDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas: {ex.Message}");
            }
        }

        public async Task<bool> CreateTaskAsync(CreateTaskItemDto newTask)
        {
            if (string.IsNullOrEmpty(AuthService.Token))
                throw new UnauthorizedAccessException("Usuário não autenticado!");

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthService.Token);
                var content = new StringContent(JsonSerializer.Serialize(newTask), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/task", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar tarefa: {ex.Message}");
            }
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            if (string.IsNullOrEmpty(AuthService.Token))
                throw new UnauthorizedAccessException("Usuário não autenticado!");

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthService.Token);
                var response = await _httpClient.DeleteAsync($"api/task/{taskId}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir tarefa: {ex.Message}");
            }
        }

        public async Task<bool> UpdateTaskAsync(int taskId, UpdateTaskItemDto updatedTask)
        {
            if (string.IsNullOrEmpty(AuthService.Token))
                throw new UnauthorizedAccessException("Usuário não autenticado!");

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthService.Token);
                var content = new StringContent(JsonSerializer.Serialize(updatedTask), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/task/{taskId}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar tarefa: {ex.Message}");
            }
        }

        public async Task<List<TaskItemDto>> GetTasksByStatusAsync(EnumTaskStatus status)
        {
            if (string.IsNullOrEmpty(AuthService.Token))
                throw new UnauthorizedAccessException("Usuário não autenticado!");

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthService.Token);
                var response = await _httpClient.GetAsync($"api/task/status/{status}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Erro ao carregar tarefas!");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                return JsonSerializer.Deserialize<List<TaskItemDto>>(jsonResponse, opt) ?? new List<TaskItemDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas: {ex.Message}");
            }
        }

    }
}
