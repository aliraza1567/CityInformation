using CityInformation.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CityInformation.Database.Repository
{
    public class CityInfoRepository: ICityInfoRepository
    {
        private readonly CityInformationContext _context;

        public CityInfoRepository(CityInformationContext context)
        {
            _context = context;
        }

        public IEnumerable<CityEntity> GetCities()
        {
            return _context.Cities.OrderBy(entity => entity.Name).ToList();
        }

        public CityEntity GetCity(int cityId, bool includePointOfInterestId)
        {
            if (includePointOfInterestId)
            {
                return _context.Cities.Include(city => city.PointsOfInterest)
                    .FirstOrDefault(city => city.CityId == cityId);
            }

            return _context.Cities.FirstOrDefault(city => city.CityId == cityId);
        }

        public IEnumerable<PointOfInterestEntity> GetPointOfInterest(int cityId)
        {
            return _context.PointOfInterests.Where(city => city.CityId == cityId);
        }

        public PointOfInterestEntity GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            return _context.PointOfInterests.FirstOrDefault(city => city.CityId == cityId && city.PointOfInterestId == pointOfInterestId);
        }

        public bool IsCityExist(int cityId)
        {
            return _context.Cities.Any(entity => entity.CityId == cityId);
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterestEntity pointOfInterestEntity)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterestEntity);
        }

        public bool Save()
        {
            return _context.SaveChanges() >=0;
        }

        public void DeletePointOfInterest(PointOfInterestEntity pointOfInterestEntity)
        {
            _context.PointOfInterests.Remove(pointOfInterestEntity);
        }
    }
}