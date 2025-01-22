using Microsoft.AspNetCore.Mvc;

namespace PetFamily.API.Controllers.SpeciesManagement.Requests
{
    public class SpeciesController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create()
        {

        }
    }
}
