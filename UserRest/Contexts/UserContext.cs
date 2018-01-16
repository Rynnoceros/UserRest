using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UserRest.Models;

namespace UserRest.Contexts
{
    public class UserContext : DbContext
    {
        public virtual DbSet<Pseudo> Pseudos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pseudo>(entity =>
            {
                entity.ToTable("pseudos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.Name).HasColumnName("name");
            });
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options) {}
    }
}
