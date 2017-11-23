using CityInformation.Database.Entities;
using System.Collections.Generic;

namespace CityInformation.Database.Repository
{
    public interface ICityInfoRepository
    {
        IEnumerable<CityEntity> GetCities();
        CityEntity GetCity(int cityId, bool includePointOfInterestId);
        IEnumerable<PointOfInterestEntity> GetPointOfInterest(int cityId);
        PointOfInterestEntity GetPointOfInterest(int cityId, int pointOfInterestId);
        bool IsCityExist(int cityId);
        void AddPointOfInterestForCity(int cityId, PointOfInterestEntity pointOfInterestEntity);
        bool Save();
        void DeletePointOfInterest(PointOfInterestEntity pointOfInterestEntity);

    }
}
