using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recrutamento.Domain.Models;
using Recrutamento.Infra;

namespace Recrutamento.API.Settings
{
    public static class ContextSettings
    {
        public static void AddContextSettings(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<RecrutamentoDbContext>(options => 
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services
                .AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<RecrutamentoDbContext>();
        }
    }
}
