using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PetFamily.Infrastructure.Options;

namespace PetFamily.API.Controllers.Volunteers
{
    public class PetController : ApplicationController
    {
        private readonly MinioOptions _minioOptions;

        public PetController(IOptions<MinioOptions> minioOptions)
        {
            _minioOptions = minioOptions.Value;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(IFormFile file)
        {
            return Ok(_minioOptions.Endpoint);
        }
    }
}
