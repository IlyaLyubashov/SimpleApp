using FakeCMS.DAL;
using FakeCMS.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static void AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(opts => IdentityOptionsConfigure(opts))
                .AddEntityFrameworkStores<DbContextFakeCms>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(opts =>  AuthenticationOptionsConfigure(opts))
                .AddJwtBearer(opts => JwtBearerOptionsConfigure(opts, configuration));
        }

        private static void JwtBearerOptionsConfigure(JwtBearerOptions opts, IConfiguration configuration)
        {
            opts.RequireHttpsMetadata = false;
            opts.SaveToken = true;

            var jwtConfiguration = configuration.GetSection("JwtSettings");
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                //ValidIssuer = jwtConfiguration["Issuer"],
                //ValidAudience = jwtConfiguration["Audience"],
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration["Key"])),
                RequireExpirationTime = false
            };
        }

        private static void AuthenticationOptionsConfigure(AuthenticationOptions opts)
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private static void IdentityOptionsConfigure(IdentityOptions opts)
        {
            opts.SignIn.RequireConfirmedAccount = false;
        }
    }
}
