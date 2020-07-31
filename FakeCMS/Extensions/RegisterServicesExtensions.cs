using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Services;
using FakeCMS.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeCMS.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            RegisterTransient(services);
        }

        private static void RegisterTransient(IServiceCollection services)
        {
            services.AddTransient<IRepository, FakeCmsRepository>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IStateService, StateService>();
        }
    }
}
