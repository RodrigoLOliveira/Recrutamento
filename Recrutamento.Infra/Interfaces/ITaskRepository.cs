using Recrutamento.Domain.Models;
using Recrutamento.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutamento.Infra.Interfaces
{
    public interface ITaskRepository : IRecrutamentoRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByUserAsync(string userId);
    }
}
