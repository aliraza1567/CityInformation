using CityInformation.Database;
using Microsoft.AspNetCore.Mvc;

namespace CityInformation.Api.Controllers
{
    public class DummyController: Controller
    {
        public CityInformationContext Context;

        public DummyController(CityInformationContext cityInformationContext)
        {
            Context = cityInformationContext;
        }

        [HttpGet("api/testdatabase")]
        public IActionResult GetCity(int cityId)
        {
           

            return Ok();
        }
    }
}
