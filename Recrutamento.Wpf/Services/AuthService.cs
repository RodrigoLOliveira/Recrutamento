using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Recrutamento.Wpf.Services
{
    public class AuthService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7058/")
        };

        public static string Token { get; private set; } = string.Empty;

        public static async Task<bool> LoginAsync(string email, string password)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { Email = email, Password = password }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(jsonResponse);

            // Armazena o token JWT
            Token = json.RootElement.GetProperty("token").GetString()!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            return true;
        }

        public static bool IsAuthenticated() => !string.IsNullOrEmpty(Token);
    }
}
