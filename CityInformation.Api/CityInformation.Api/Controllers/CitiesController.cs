using CityInformation.Api.Models;
using CityInformation.Database.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CityInformation.Api.Controllers
{
    [Route("api/cities")]
    public class CitiesController: Controller
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

            var result = new List<CityWithoutPointOfInterestDto>();

            

            return Ok(result);
        }

        [HttpGet("{cityId}")]
        public IActionResult GetCity(int cityId, bool pointOfInterest=false)
        {
            var cityEntity = _cityInfoRepository.GetCity(cityId, pointOfInterest);

            if (cityEntity == null)
            {
                return NotFound();
            }

            if (pointOfInterest)
            {
                var resultWithPointOfInterest = new CityWithPointOfInterestDto
                {
                    CityId = cityEntity.CityId,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                };

                foreach (var pointOfInterestEntity in cityEntity.PointOfInterest)
                {
                    resultWithPointOfInterest.PointsOfInterest.Add(new PointsOfInterestReponseDto
                    {
                        PointsOfInterestId = pointOfInterestEntity.CityId,
                        Name = pointOfInterestEntity.Name,
                        Description = pointOfInterestEntity.Description
                    });
                }

                return Ok(resultWithPointOfInterest);
            }

            var resultWithOutPointOfInterest = new CityWithoutPointOfInterestDto()
            {
                CityId = cityEntity.CityId,
                Name = cityEntity.Name,
                Description = cityEntity.Description
            };


            return Ok(resultWithOutPointOfInterest);
        }
    }
}
