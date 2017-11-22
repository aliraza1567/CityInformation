using CityInformation.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInformation.Database
{
    public class CityInformationContext: DbContext
    {
        public CityInformationContext(DbContextOptions<CityInformationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterests { get; set; }
    }
}
