using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recrutamento.Domain.Enums;
using Recrutamento.Domain.Models;

namespace Recrutamento.Infra.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder
                .Property(e => e.Status)
                .HasConversion(
                    e => e.ToString(),
                    e => (EnumTaskStatus) Enum.Parse(typeof(EnumTaskStatus), e)
                );
        }
    }
}
