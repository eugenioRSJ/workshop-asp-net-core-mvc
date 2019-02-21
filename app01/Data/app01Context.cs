using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace app01.Models
{
    public class app01Context : DbContext
    {
        public app01Context (DbContextOptions<app01Context> options)
            : base(options)
        {
        }

        public DbSet<app01.Models.Department> Department { get; set; }
    }
}
