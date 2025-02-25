using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers.FileProvider;
using FileInfo = PetFamily.Application.Providers.FileProvider.FileInfo;

namespace PetFamily.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly ILogger<FilesCleanerBackgroundService> _logger;

    public FilesCleanerBackgroundService(
        IServiceScopeFactory scopeFactory, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        ILogger<FilesCleanerBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _messageQueue = messageQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();

            var files = await _messageQueue.ReadAsync(stoppingToken);
            var deleteResult = await fileProvider.Delete(files, stoppingToken);

            var paths = string.Join(", ", deleteResult.Value);

            _logger.LogInformation(
                "The file(s) named '{name}' has been successfully deleted from MinIO " +
                "by the 'File Cleaner Background Service'", paths);
        }
    }
}
