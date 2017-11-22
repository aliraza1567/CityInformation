using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInformation.Database.Entities
{
    public class PointOfInterestEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PointOfInterestId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        [ForeignKey("CityId")]
        public CityEntity City { get; set; }
        public int CityId { get; set; }

    }
}