using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityInformation.Api.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestController: Controller
    {
        [HttpGet("{CityId}/PointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var cityInfo = CitiesDataStore.CurrentDataStore.Cities.FirstOrDefault(c => c.CityId == cityId);

            if (cityInfo == null)
            {
                return NotFound();
            }

            return Ok(cityInfo);
        }

        [HttpGet("{CityId}/PointOfInterest/{pointOfInterestId}")]
        public IActionResult GetPointsOfInterest(int cityId, int pointOfInterestId)
        {
            var cityInfo = CitiesDataStore.CurrentDataStore.Cities.FirstOrDefault(c => c.CityId == cityId);

            if (cityInfo == null)
            {
                return NotFound();
            }

            var pointOfInterest = cityInfo.PointsOfInterest.FirstOrDefault(c => c.PointsOfInterestId == pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }


            return Ok(pointOfInterest);
        }

    }
}
