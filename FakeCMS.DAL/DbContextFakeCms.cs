using System;
using FakeCMS.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FakeCMS.DAL
{
    public class DbContextFakeCms : IdentityDbContext<User, Role, int>
    {
        public DbContextFakeCms(DbContextOptions<DbContextFakeCms> options) : base(options)
        {
            //this.Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<RoleLink> RoleLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.UserName);

            modelBuilder.Entity<RoleLink>()
                .HasOne(rl => rl.ParentRole)
                .WithMany(r => r.LinksToChildren)
                .HasForeignKey(rl => rl.ParentRoleId);

            modelBuilder.Entity<RoleLink>()
                .HasOne(rl => rl.ChildRole)
                .WithMany(r => r.LinksToParents)
                .HasForeignKey(rl => rl.ChildRoleId);
        }
    }
    
}
