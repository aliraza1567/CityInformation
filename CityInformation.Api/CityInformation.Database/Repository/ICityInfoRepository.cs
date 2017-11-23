using CityInformation.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CityInformation.Database.Repository
{
    public interface ICityInfoRepository
    {
        IEnumerable<CityEntity> GetCities();
        CityEntity GetCity(int cityId, bool includePointOfInterestId);
        IEnumerable<PointOfInterestEntity> GetPointOfInterest(int cityId);
        PointOfInterestEntity GetPointOfInterest(int cityId, int pointOfInterestId);
        bool IsCityExist(int cityId);

    }

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
                return _context.Cities.Include(city => city.PointOfInterest)
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
    }
}
