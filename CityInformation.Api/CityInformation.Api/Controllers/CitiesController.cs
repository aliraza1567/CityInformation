using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CityInformation.Api.Controllers
{
    public class CitiesController: Controller
    {
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<object>
                {
                    new {id=1, Name = "Amsterdam"},
                    new {id=2, Name = "New York"}
                }
                );
        }
    }
}
