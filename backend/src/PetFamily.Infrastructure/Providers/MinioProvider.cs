﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Application.VolunteersManagement.DeleteFiles;
using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Application.VolunteersManagement.GetFiles;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private const int MAX_DEGREE_OF_PARALLELISM = 5;

        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<List<FilePath>, Error>> Upload(
            IEnumerable<UploadFileDto> files, 
            string bucketName,
            CancellationToken cancellationToken)
        {
            var fileList = files.ToList();
            var semaphore = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);

            if(await IsBucketExtst(bucketName, cancellationToken) == false)
                await CreateBucket(bucketName, cancellationToken);

            List<FilePath> objectNames = [];

            foreach(var file in fileList)
            {
                var task = await PutObject(file, bucketName, semaphore, cancellationToken);
                if(task.IsFailure)
                    return task.Error;

                objectNames.Add(task.Value);
            }

            return objectNames;
        }


        public async Task<Result<string, Error>> GetByName(
            GetFilesCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var resultBucketExist = await IsBucketExtst(command.BucketName, cancellationToken);

                if(resultBucketExist == false)
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
                _logger.LogError(ex, "Fail to get a file from MinIO with name: {name}, bucket: {bucket} in MinIO", 
                    command.ObjectName, command.BucketName);

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
                _logger.LogError(ex, "Fail to delete a file from MinIO with name: {name}, bucket: {bucket} in MinIO",
                    command.ObjectName, command.BucketName);

                return Error.Failure("file.delete", "File deleting error");
            }
        }


        private async Task<bool> IsBucketExtst(string bucketName, CancellationToken cancellationToken)
        {
            var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);

            return await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
        }


        private async Task CreateBucket(string bucketName, CancellationToken cancellationToken)
        {
            var newBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(newBucketArgs, cancellationToken);
        }


        private async Task<Result<FilePath, Error>> PutObject(
            UploadFileDto file, 
            string bucketName, 
            SemaphoreSlim semaphore,
            CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync();

            PutObjectArgs objectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(file.Stream)
                .WithObjectSize(file.Stream.Length)
                .WithObject(file.ObjectName.Value);

            try
            {
                await _minioClient.PutObjectAsync(objectArgs, cancellationToken);
                return file.ObjectName;
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Fail to put a file with name: {name} in bucket: {bucket} in MinIO", 
                    file.ObjectName.Value, bucketName);

                return Error.Failure("file.upload", "File upload error, check file name or bucket name");
            }

            finally
            {
                semaphore.Release();
            }
        }
    }
}
