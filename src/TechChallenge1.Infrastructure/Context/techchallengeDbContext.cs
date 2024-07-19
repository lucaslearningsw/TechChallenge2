using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TechChallenge1.Domain.Models;


namespace TechChallenge1.Data.Context
{
    public sealed class techchallengeDbContext(DbContextOptions<techchallengeDbContext> options)
    : DbContext(options)
    {
       
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(techchallengeDbContext).Assembly);

           // modelBuilder.Entity<State>().HasData(StateList.List);
        }

    }
}
