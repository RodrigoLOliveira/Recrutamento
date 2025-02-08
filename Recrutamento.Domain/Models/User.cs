using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutamento.Domain.Models
{
    public class User : IdentityUser
    {
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
