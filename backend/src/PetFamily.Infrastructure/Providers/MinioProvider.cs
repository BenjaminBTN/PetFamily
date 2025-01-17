using CSharpFunctionalExtensions;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.Dtos;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private readonly IMinioClient _minioClient;

        public MinioProvider(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task<Result<string, Error>> Upload(FileDto fileDto, CancellationToken cancellationToken)
        {
            var path = Guid.NewGuid().ToString();

            try
            {
                PutObjectArgs objectArgs = new PutObjectArgs()
                .WithBucket(fileDto.Bucket)
                .WithStreamData(fileDto.Stream)
                .WithObjectSize(fileDto.Stream.Length)
                .WithObject("NewFile");

                var result = await _minioClient.PutObjectAsync(objectArgs, cancellationToken);
            }
            catch(Exception ex)
            {
                return Error.Failure("file.upload", "File upload error");
            }

            return 
        }
    }
}
