using AutoMapper;
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

            var result = Mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities);

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
                var resultWithPointOfInterest = Mapper.Map<CityWithPointOfInterestDto>(cityEntity);

                return Ok(resultWithPointOfInterest);
            }

            var resultWithOutPointOfInterest = Mapper.Map<CityWithoutPointOfInterestDto>(cityEntity);


            return Ok(resultWithOutPointOfInterest);
        }
    }
}
