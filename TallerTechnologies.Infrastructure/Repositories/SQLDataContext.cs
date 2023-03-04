using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerTechnologies.Test.Infrastructure.Models;
using System.Reflection.Metadata;

namespace TallerTechnologies.Test.Infrastructure.Repository
{
    public class SQLDataContext : DbContext
    {
        IConfiguration _configuration;
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Color> Colors { get; set; }

        public SQLDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = _configuration.GetConnectionString("CarsDB");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Car>().Property(s => s.Make).HasMaxLength(50);
            //modelBuilder.Entity<Car>().Property(s => s.Model).HasMaxLength(50);
            modelBuilder.Entity<Car>().HasOne(s => s.Color);
        }
    }
}
