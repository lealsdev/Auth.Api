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
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
