using FakeCMS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.TableManagment
{
    public static partial class ModelBuilderExtensions
    {
        public static void ManageBoardEntities(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ObjectState>()
                .HasKey(os => new { os.TableId, os.ObjectId });

            modelBuilder.Entity<StateTable>()
                .HasKey(st => new { st.TableId, st.StateId });

            modelBuilder.Entity<RoleState>()
                .HasKey(rs => new { rs.RoleId, rs.StateId });
        }
    }
}
