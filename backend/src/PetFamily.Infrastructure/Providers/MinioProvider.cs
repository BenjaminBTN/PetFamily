using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Application.Volunteers.AddFiles;
using PetFamily.Application.Volunteers.DeleteFiles;
using PetFamily.Application.Volunteers.GetFiles;
using PetFamily.Domain.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PetFamily.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<string, Error>> Upload(AddFilesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var resultBucketExist = await IsBucketExtst(command.BucketName, cancellationToken);

                if(resultBucketExist is false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                        .WithBucket(command.BucketName);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                }

                PutObjectArgs objectArgs = new PutObjectArgs()
                .WithBucket(command.BucketName)
                .WithStreamData(command.Stream)
                .WithObjectSize(command.Stream.Length)
                .WithObject(command.ObjectName);

                var result = await _minioClient.PutObjectAsync(objectArgs, cancellationToken);

                return result.ObjectName;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Fail to upload a file to MinIO");
                return Error.Failure("file.upload", "File upload error");
            }
        }

        public async Task<Result<string, Error>> GetByName(
            GetFilesCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var resultBucketExist = await IsBucketExtst(command.BucketName, cancellationToken);

                if(resultBucketExist is false)
                {
                    _logger.LogError("Fail to get a file from MinIO due to missing bucket");
                    return Error.Failure("file.upload", "File getting error");
                }

                var args = new PresignedGetObjectArgs()
                .WithBucket(command.BucketName)
                .WithObject(command.ObjectName)
                .WithExpiry(3600);

                return await _minioClient.PresignedGetObjectAsync(args);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Fail to get a file from MinIO");
                return Error.Failure("file.upload", "File getting error");
            }

            
        }

        public async Task<Result<string, Error>> Delete(
            DeleteFilesCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var args = new RemoveObjectArgs()
                .WithBucket(command.BucketName)
                .WithObject(command.ObjectName);

                await _minioClient.RemoveObjectAsync(args);

                return command.ObjectName;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Fail to get a file from MinIO");
                return Error.Failure("file.upload", "File getting error");
            }
        }


        private async Task<bool> IsBucketExtst(string bucketName, CancellationToken cancellationToken)
        {
            var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);

            return await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
        }
    }
}
