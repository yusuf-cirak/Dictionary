using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Persistence.Context;
using Dictionary.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DictionaryContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DictionarySqlServer"), sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure();
                });
            });


            
            SeedData.SeedAsync(configuration).GetAwaiter().GetResult(); // Comment this before migration


            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
