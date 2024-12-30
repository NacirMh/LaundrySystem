using LaundrySystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LaundrySystem.WebApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Cycle> Cycles{ get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Laundry> Laveries { get; set; }
        public DbSet<Actionn> Actions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Owner>()
                .HasIndex(c => new { c.Name })
                .IsUnique(true);
        }


    }
}
