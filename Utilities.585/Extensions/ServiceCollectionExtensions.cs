using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utilities._585.Data.Repositories;
using Utilities._585.Data.UnitOfWorks;
using Utilities._585.Helpers;
using Utilities._585.Models;

namespace Utilities._585.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

        public static IServiceCollection AddSmtpEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SectionName));
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
