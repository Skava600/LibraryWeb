using LibraryWeb.Contracts.Config;
using LibraryWeb.Core.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MediatR;
using System.Reflection;
using FluentValidation;

namespace LibraryWeb.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<JwtTokenConfig>(configuration.GetSection("JwtTokenConfig"))
               .AddSingleton(x => x.GetRequiredService<IOptions<JwtTokenConfig>>().Value);

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
                    // for further customization
                    // options.OperationFilter<DefaultValuesFilter>();
                });
            return services;
        }

        public static IServiceCollection AddJwtBearerAuth(this IServiceCollection services) 
        {

            services.ConfigureOptions<ConfigureJwtBearerOptions>()
                .AddAuthentication()
                .AddJwtBearer();

            return services;
        }
    }
}
