using System.Collections.Generic;

namespace CityInformation.Api.Models
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberOfPointsOfInterest => PointsOfInterest.Count;

        public ICollection<PointsOfInterestReponseDto> PointsOfInterest { get; set; } = new List<PointsOfInterestReponseDto>();
    }
}
