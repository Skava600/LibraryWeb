using LibraryWeb.Core.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation;
using AutoMapper;

namespace LibraryWeb.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
                
        }

        public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>()
                .AddSwaggerGen(options =>
                {
                });
            return services;
        }
    }
}
