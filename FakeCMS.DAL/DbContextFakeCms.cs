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
            
        }

        public DbSet<Item> Items { get; set; }
    }
    
}
