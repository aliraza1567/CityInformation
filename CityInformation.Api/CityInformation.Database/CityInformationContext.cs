using CityInformation.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInformation.Database
{
    public class CityInformationContext: DbContext
    {
        public CityInformationContext(DbContextOptions<CityInformationContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<PointOfInterestEntity> PointOfInterests { get; set; }
    }
}
