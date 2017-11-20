using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityInformation.Api.Controllers
{
    [Route("api/cities")]
    public class CitiesController: Controller
    {

        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.CurrentDataStore.Cities);
        }

        [HttpGet("{cityId}")]
        public IActionResult GetCity(int cityId)
        {
            var  cityInfo = CitiesDataStore.CurrentDataStore.Cities.FirstOrDefault(c => c.CityId == cityId);

            if (cityInfo == null)
            {
                return NotFound();
            }

            return Ok(cityInfo);
        }
    }
}
