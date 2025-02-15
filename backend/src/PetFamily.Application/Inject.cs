using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.SpeciesManagement.AddBreed;
using PetFamily.Application.SpeciesManagement.Create;
using PetFamily.Application.VolunteersManagement.Commands.AddPet;
using PetFamily.Application.VolunteersManagement.Commands.AddPetPhotos;
using PetFamily.Application.VolunteersManagement.Commands.Create;
using PetFamily.Application.VolunteersManagement.Commands.DeleteFiles;
using PetFamily.Application.VolunteersManagement.Commands.GetFiles;
using PetFamily.Application.VolunteersManagement.Commands.HardDelete;
using PetFamily.Application.VolunteersManagement.Commands.MovePet;
using PetFamily.Application.VolunteersManagement.Commands.SoftDelete;
using PetFamily.Application.VolunteersManagement.Commands.Update.MainInfo;
using PetFamily.Application.VolunteersManagement.Commands.Update.Requsites;
using PetFamily.Application.VolunteersManagement.Commands.Update.SocialNetworks;
using PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination;

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
            services.AddScoped<GetAllVolunteersWithPaginationHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
