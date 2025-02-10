using Microsoft.EntityFrameworkCore;
using Recrutamento.Domain.Models;
using Recrutamento.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutamento.Infra.Repositories
{
    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(RecrutamentoDbContext context) : base(context) { }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserAsync(string userId)
        {
            return await _dbSet.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}
