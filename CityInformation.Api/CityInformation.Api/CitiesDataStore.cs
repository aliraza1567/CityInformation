using CityInformation.Api.Models;
using System.Collections.Generic;

namespace CityInformation.Api
{
    public class CitiesDataStore
    {
        public static CitiesDataStore CurrentDataStore { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                new CityDto
                {
                    CityId = 1,
                    Name = "Amsterdam",
                    Description = "Amsterdam is the Netherlands’ capital, known for its artistic heritage."
                },

                new CityDto
                {
                    CityId = 2,
                    Name = "Antwerp",
                    Description = "Antwerp is a port city on Belgium’s River Scheldt, with history dating to the Middle Ages."
                },

                new CityDto
                {
                    CityId = 3,
                    Name = "New York",
                    Description = "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean."
                }
            };
        }
    }
}
