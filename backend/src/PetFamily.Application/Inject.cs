using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.SpeciesManagement.AddBreed;
using PetFamily.Application.SpeciesManagement.Create;
using PetFamily.Application.VolunteersManagement.AddPet;
using PetFamily.Application.VolunteersManagement.AddPetPhotos;
using PetFamily.Application.VolunteersManagement.Create;
using PetFamily.Application.VolunteersManagement.DeleteFiles;
using PetFamily.Application.VolunteersManagement.GetFiles;
using PetFamily.Application.VolunteersManagement.HardDelete;
using PetFamily.Application.VolunteersManagement.MovePet;
using PetFamily.Application.VolunteersManagement.SoftDelete;
using PetFamily.Application.VolunteersManagement.Update.MainInfo;
using PetFamily.Application.VolunteersManagement.Update.Requsites;
using PetFamily.Application.VolunteersManagement.Update.SocialNetworks;

namespace PetFamily.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<UpdateSocialNetworksHandler>();
            services.AddScoped<UpdateRequsitesHandler>();
            services.AddScoped<SoftDeleteVolunteerHandler>();
            services.AddScoped<HardDeleteVolunteerHandler>();
            services.AddScoped<AddPetHandler>();
            services.AddScoped<AddPetPhotosHandler>();
            services.AddScoped<GetFilesHandler>();
            services.AddScoped<DeleteFilesHandler>();
            services.AddScoped<CreateSpeciesHandler>();
            services.AddScoped<AddBreedHandler>();
            services.AddScoped<MovePetHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
