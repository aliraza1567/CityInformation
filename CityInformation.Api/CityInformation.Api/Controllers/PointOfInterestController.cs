﻿using AutoMapper;
using CityInformation.Api.Models;
using CityInformation.Api.Services;
using CityInformation.Database.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CityInformation.Api.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestController: Controller
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{CityId}/PointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            if (!_cityInfoRepository.IsCityExist(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntities = _cityInfoRepository.GetPointOfInterest(cityId);

            if (pointOfInterestEntities == null)
            {
                return NotFound();
            }

            var pointsOfInterest = Mapper.Map<IEnumerable<PointsOfInterestReponseDto>>(pointOfInterestEntities);

            return Ok(pointsOfInterest);
        }

        [HttpGet("{CityId}/PointOfInterest/{pointOfInterestId}", Name = "GetPointsOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId, int pointOfInterestId)
        {
            if (!_cityInfoRepository.IsCityExist(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointsOfInterest = Mapper.Map<PointsOfInterestReponseDto>(pointOfInterestEntity);

            return Ok(pointsOfInterest);
        }

        [HttpPost("{cityId}/PointOfInterest")]
        public IActionResult CreatePointsOfInterest(int cityId, [FromBody]PointsOfInterestRequestDto pointsOfInterestRequestDto)
        {
            if (pointsOfInterestRequestDto == null)
            {
                return BadRequest();
            }

            if (pointsOfInterestRequestDto.Name == pointsOfInterestRequestDto.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can not be the Same"); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.CurrentDataStore.Cities.FirstOrDefault(c => c.CityId == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointsOfInterestId = CitiesDataStore.CurrentDataStore.Cities.SelectMany(p => p.PointsOfInterest)
                .Max(dto => dto.PointsOfInterestId);

            var pointOfInterest = new PointsOfInterestReponseDto
            {
                PointsOfInterestId = ++maxPointsOfInterestId,
                Name = pointsOfInterestRequestDto.Name,
                Description = pointsOfInterestRequestDto.Description
            };

            city.PointsOfInterest.Add(pointOfInterest);

            return CreatedAtRoute("GetPointsOfInterest", new
            {
                CityId = cityId,
                pointOfInterestId = pointOfInterest.PointsOfInterestId
            }, pointOfInterest);

        }

        [HttpPut("{cityId}/PointOfInterest/{pointOfInterestId}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, [FromBody]PointsOfInterestRequestDto pointsOfInterestRequestDto)
        {
            if (pointsOfInterestRequestDto == null)
            {
                return BadRequest();
            }

            if (pointsOfInterestRequestDto.Name == pointsOfInterestRequestDto.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can not be the Same");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.CurrentDataStore.Cities.FirstOrDefault(c => c.CityId == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointsOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.PointsOfInterestId == pointOfInterestId);

            if (pointsOfInterest == null)
            {
                return NotFound();
            }

            pointsOfInterest.Name = pointsOfInterestRequestDto.Name;
            pointsOfInterest.Description = pointsOfInterestRequestDto.Description;

            return NoContent();
        }

        [HttpPatch("{cityId}/PointOfInterest/{pointOfInterestId}")]
        public IActionResult PatchPointOfInterest(int cityId, int pointOfInterestId, [FromBody]JsonPatchDocument<PointsOfInterestRequestDto> pointsOfInterestPatchDoc)
        {
            if (pointsOfInterestPatchDoc == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.CurrentDataStore.Cities.FirstOrDefault(c => c.CityId == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointsOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.PointsOfInterestId == pointOfInterestId);

            if (pointsOfInterest == null)
            {
                return NotFound();
            }

            var patchPointOfInterest = new PointsOfInterestRequestDto
            {
                Name = pointsOfInterest.Name,
                Description = pointsOfInterest.Description
            };

            pointsOfInterestPatchDoc.ApplyTo(patchPointOfInterest, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (patchPointOfInterest.Name == patchPointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can not be the Same");
            }

            TryValidateModel(patchPointOfInterest);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointsOfInterest.Name = patchPointOfInterest.Name;
            pointsOfInterest.Description = patchPointOfInterest.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/PointOfInterest/{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {

            var city = CitiesDataStore.CurrentDataStore.Cities.FirstOrDefault(c => c.CityId == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointsOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.PointsOfInterestId == pointOfInterestId);

            if (pointsOfInterest == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointsOfInterest);

            _mailService.Send("Delete", $"PointOfInterest: {pointsOfInterest.PointsOfInterestId} has been deleted for City: {city.CityId}");
            _logger.LogCritical($"PointOfInterest: {pointsOfInterest.PointsOfInterestId} has been deleted for City: {city.CityId}");
            
            return NoContent();
        }
    }
}
