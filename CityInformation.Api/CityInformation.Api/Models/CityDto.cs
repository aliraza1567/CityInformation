namespace CityInformation.Api.Models
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest { get; set; }
    }
}
