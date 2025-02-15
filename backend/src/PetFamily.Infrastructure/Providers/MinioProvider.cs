using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Application.VolunteersManagement.Commands.GetFiles;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;
using FileInfo = PetFamily.Application.Providers.FileProvider.FileInfo;

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

        public async Task<Result<List<FilePath>, Error>> UploadObjects(
            IEnumerable<UploadFileData> files, 
            string bucketName,
            CancellationToken cancellationToken)
        {
            var fileList = files.ToList();
            var semaphore = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);

            if(await IsBucketExtst(bucketName, cancellationToken) == false)
                await CreateBucket(bucketName, cancellationToken);

            List<Task> taskList = [];
            List<Result<FilePath, Error>> filePathsResult = [];

            foreach(var file in fileList)
            {
                var task = Task.Run(async () =>
                {
                    var putObjectResult = await PutObject(file, bucketName, semaphore, cancellationToken);
                    filePathsResult.Add(putObjectResult);
                }, 
                cancellationToken);

                taskList.Add(task);
            }
            await Task.WhenAll(taskList);

            if(filePathsResult.Any(r => r.IsFailure))
                return filePathsResult.FirstOrDefault(r => r.IsFailure).Error;

            List<FilePath> filePaths = [];
            foreach(var result in filePathsResult)
            {
                filePaths.Add(result.Value);
            }

            return filePaths;
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
                    _logger.LogError("Fail to get a file from MinIO due to missing bucket with name '{name}'",
                        command.BucketName);
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
                _logger.LogError(ex, "Fail to get a file from MinIO with name '{name}', bucket '{bucket}'", 
                    command.ObjectName, command.BucketName);

                return Error.Failure("file.upload", "File getting error");
            }
        }


        public async Task<Result<List<string>, Error>> Delete(
            IEnumerable<FileInfo> files,
            CancellationToken cancellationToken)
        {
            foreach(var file in files)
            {
                if(await IsBucketExtst(file.BucketName, cancellationToken) == false)
                {
                    _logger.LogError(
                        "Fail to delete a file(s) from MinIO, the bucket '{bucket}' does not exist",
                        file.BucketName);

                    return Error.Failure("file.delete",
                        "It is not possible to delete the file due to the absence of the bucket");
                }

                var result = await RemoveObject(file.ObjectName, file.BucketName, cancellationToken);
                if(result.IsFailure)
                    return result.Error;
            }

            return files.Select(f => f.ObjectName).ToList();
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
            UploadFileData file, 
            string bucketName, 
            SemaphoreSlim semaphore,
            CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync(cancellationToken);

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
                _logger.LogError(ex, "Fail to put a file with name '{name}' in the bucket '{bucket}' in MinIO",
                    file.ObjectName.Value, bucketName);

                return Error.Failure("file.upload", "File upload error, check the file name or the bucket name");
            }
            finally
            {
                semaphore.Release();
            }
        }


        private async Task<Result<string, Error>> RemoveObject(
            string objectName,
            string bucketName,
            CancellationToken cancellationToken)
        {
            try
            {
                var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

                await _minioClient.RemoveObjectAsync(args, cancellationToken);

                return objectName;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Fail to delete a file from MinIO with name '{name}', bucket '{bucket}'",
                    objectName, bucketName);

                return Error.Failure("file.delete", "File deleting error");
            }
        }
    }
}
