using AutoMapper;
using CityInformation.Api.Models;
using CityInformation.Api.Services;
using CityInformation.Database.Entities;
using CityInformation.Database.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

            if (!_cityInfoRepository.IsCityExist(cityId))
            {
                return NotFound();
            }

            var pointOfIntrest = Mapper.Map<PointOfInterestEntity>(pointsOfInterestRequestDto);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, pointOfIntrest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occured while handlig the request");
            }

            var pointOfIntrestRetrun = Mapper.Map<PointsOfInterestReponseDto>(pointOfIntrest);

            return CreatedAtRoute("GetPointsOfInterest", new
            {
                CityId = cityId,
                pointOfInterestId = pointOfIntrestRetrun.PointOfInterestId
            }, pointOfIntrestRetrun);

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

            if (!_cityInfoRepository.IsCityExist(cityId))
            {
                return NotFound();
            }

            var pointsOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityId, pointOfInterestId);

            if (pointsOfInterestEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(pointsOfInterestRequestDto, pointsOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occured while handlig the request");
            }

            return NoContent();
        }

        [HttpPatch("{cityId}/PointOfInterest/{pointOfInterestId}")]
        public IActionResult PatchPointOfInterest(int cityId, int pointOfInterestId, [FromBody]JsonPatchDocument<PointsOfInterestRequestDto> pointsOfInterestPatchDoc)
        {
            if (pointsOfInterestPatchDoc == null)
            {
                return BadRequest();
            }

            if (!_cityInfoRepository.IsCityExist(cityId))
            {
                return NotFound();
            }

            var pointsOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityId, pointOfInterestId);

            if (pointsOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointsOfInterestPatch = Mapper.Map<PointsOfInterestRequestDto>(pointsOfInterestEntity);

            pointsOfInterestPatchDoc.ApplyTo(pointsOfInterestPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointsOfInterestPatch.Name == pointsOfInterestPatch.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can not be the Same");
            }

            TryValidateModel(pointsOfInterestPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(pointsOfInterestPatch, pointsOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occured while handlig the request");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/PointOfInterest/{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {

            if (!_cityInfoRepository.IsCityExist(cityId))
            {
                return NotFound();
            }

            var pointsOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityId, pointOfInterestId);

            if (pointsOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointsOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occured while handlig the request");
            }

            _mailService.Send("Delete", $"PointOfInterest: {pointOfInterestId} has been deleted for City: {cityId}");
            _logger.LogCritical($"PointOfInterest: {pointOfInterestId} has been deleted for City: {cityId}");
            
            return NoContent();
        }
    }
}
