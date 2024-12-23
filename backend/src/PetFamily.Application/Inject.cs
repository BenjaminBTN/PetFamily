using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.Create;

namespace PetFamily.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<IValidator, CreateVolunteerCommandValidator>();
            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
