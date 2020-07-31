using System;
using System.Linq;
using System.Reflection;
using FakeCMS.DAL.Entities;
using FakeCMS.DAL.TableManagment;
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
            this.Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }

        //public DbSet<RoleLink> RoleLinks { get; set; }

        public DbSet<StateTable> StateTables { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<ObjectState> ObjectStates { get; set; }

        public DbSet<RoleState> RoleStates { get; set; }

        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.UserName);

            //modelBuilder.Entity<RoleLink>()
            //    .HasOne(rl => rl.ParentRole)
            //    .WithMany(r => r.LinksToChildren)
            //    .HasForeignKey(rl => rl.ParentRoleId);

            //modelBuilder.Entity<RoleLink>()
            //    .HasOne(rl => rl.ChildRole)
            //    .WithMany(r => r.LinksToParents)
            //    .HasForeignKey(rl => rl.ChildRoleId);

            modelBuilder.ManageBoardEntities();
        }

        
    }

    public class StatefullEntityCreater
    {
        private readonly DbContextFakeCms _dbContext;

        public StatefullEntityCreater(DbContextFakeCms dbContext)
        {
            _dbContext = dbContext;
        }

        public async void EnsureTables()
        {
            var dalAssembly = Assembly.GetAssembly(typeof(DbContextFakeCms));
            foreach (var type in dalAssembly.DefinedTypes)
            {
                var attr = type.GetCustomAttribute(typeof(StatefullEntityAttribute)) as StatefullEntityAttribute;
                if (attr == null) continue;

                var table = await _dbContext.Tables.SingleOrDefaultAsync(t => t.Name == attr.TableName);
                if (table == null)
                {
                    table = new Table
                    {
                        Name = attr.TableName,
                        TypeName = type.FullName
                    };
                    await _dbContext.AddAsync(table);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}
