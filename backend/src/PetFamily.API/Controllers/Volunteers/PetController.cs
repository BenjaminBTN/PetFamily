using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace PetFamily.API.Controllers.Volunteers
{
    public class PetController : ApplicationController
    {
        private readonly IMinioClient _minioClient;

        public PetController(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(
            IFormFile file,
            CancellationToken cancellationToken)
        {
            var buckets = await _minioClient.ListBucketsAsync(cancellationToken);
            var bucketsString = String.Join(" ", buckets.Buckets.Select(x => x.Name));

            var stream = file.OpenReadStream();

            PutObjectArgs objectArgs = new PutObjectArgs()
                .WithBucket("photos")
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithObject("NewFile");

            await _minioClient.PutObjectAsync(objectArgs);

            return bucketsString;
        }
    }
}
