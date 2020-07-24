using System;
using Microsoft.Extensions.DependencyInjection;
using Auth.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Auth.Repository.Interfaces;
using Auth.Repository;
using Auth.Application.Interfaces;
using Auth.Application;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Auth.Infra.Ioc
{
    public static class Dependency
    {
        public static IServiceCollection AddDbContextWith(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection.AddDbContext<DataContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Auth.Api")));
        }

        public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserApplication, UserApplication>()
                .AddScoped<IAuthApplication, AuthApplication>()
                .AddScoped<IBCryptApplication, BCryptApplication>()
                .AddScoped<ITokenApplication, TokenApplication>()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v1",
                        Title = "User API",
                        Description = "User API",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Auth API",
                            Email = "authapi@authapi.com",
                            Url = new Uri("https://www.example.com/"),
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = new Uri("https://example.com/license"),
                        }
                    });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                });
        }

        public static IServiceCollection AddAuthorizationAndAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateIssuerSigningKey = false,                        
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration.GetSection("AppSettings:Token:Key").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            serviceCollection.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy =>
                                policy.RequireClaim("admin"));
            });

            return serviceCollection;
        }
    }
}
