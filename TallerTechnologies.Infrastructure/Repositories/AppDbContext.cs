using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerTechnologies.Infrastructure.Models;

namespace TallerTechnologies.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            //
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //
        }

        public virtual DbSet<Car> Cars { get; set; }
    }
}
