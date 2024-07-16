using Application.Profiles;
using AutoMapper.Internal;
using Hangfire;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(MappingProfile));
            services.AddHangfire(option => option
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

            // Thêm Hangfire server
            services.AddHangfireServer();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.ConfigurePersistenceService(configuration);
            //services.AddTransient<IRequestHandler<LoginRequest, AuthResponse>, LoginRequestHandler>();
            //services.AddScoped<IDataDefaultService, DataDefaultService>();
            //services.Configure<BackupRestoreConfiguration>(configuration.GetSection("BackupRestoreConfiguration"));
            return services;
        }
    }
}
