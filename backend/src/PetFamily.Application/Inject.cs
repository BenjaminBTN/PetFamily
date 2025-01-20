using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.AddFiles;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.DeleteFiles;
using PetFamily.Application.Volunteers.GetFiles;
using PetFamily.Application.Volunteers.HardDelete;
using PetFamily.Application.Volunteers.SoftDelete;
using PetFamily.Application.Volunteers.Update.MainInfo;
using PetFamily.Application.Volunteers.Update.SocialNetworks;

namespace PetFamily.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<UpdateSocialNetworksHandler>();
            services.AddScoped<SoftDeleteVolunteerHandler>();
            services.AddScoped<HardDeleteVolunteerHandler>();
            services.AddScoped<AddPetHandler>();
            services.AddScoped<AddFilesHandler>();
            services.AddScoped<GetFilesHandler>();
            services.AddScoped<DeleteFilesHandler>();
            services.AddScoped<IValidator, CreateVolunteerCommandValidator>();
            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
