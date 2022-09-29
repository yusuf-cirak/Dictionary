using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Features.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<UserBusinessRules>();

            var executingAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(executingAssembly);

            services.AddAutoMapper(executingAssembly);

            services.AddValidatorsFromAssembly(executingAssembly);

            return services;
        }
    }
}
