using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recrutamento.Domain.Models;

namespace Recrutamento.Infra
{
    public class RecrutamentoDbContext : IdentityDbContext
    {
        public RecrutamentoDbContext(DbContextOptions<RecrutamentoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(RecrutamentoDbContext).Assembly);
        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
