using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Database;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Application.SpeciesManagement;
using PetFamily.Application.VolunteersManagement;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.MessageQueues;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using FileInfo = PetFamily.Application.Providers.FileProvider.FileInfo;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMinio(configuration);

        services.AddHostedService<FilesCleanerBackgroundService>();

        services.AddSingleton<
            IMessageQueue<IEnumerable<FileInfo>>,
            MemoryMessageQueue<IEnumerable<FileInfo>>>();

        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }


    private static IServiceCollection AddMinio(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                ?? throw new ApplicationException("Missing MinIO configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(false);
        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }
}
