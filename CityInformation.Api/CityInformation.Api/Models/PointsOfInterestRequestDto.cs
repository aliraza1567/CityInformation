using System.ComponentModel.DataAnnotations;

namespace CityInformation.Api.Models
{
    public class PointsOfInterestRequestDto
    {
        [Required(ErrorMessage = "Name is Required for PointsOfInterest.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(2000)]
        [Required(ErrorMessage = "Description is Required for PointsOfInterest.")]
        public string Description { get; set; }
    }
}
