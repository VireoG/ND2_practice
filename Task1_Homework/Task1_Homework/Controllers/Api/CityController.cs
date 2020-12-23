using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Filters;

namespace Task1_Homework.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<City>), StatusCodes.Status200OK)]
        public IActionResult GetCities()
        {
            return Ok(cityService.GetCities().ToList());
        }
    }
}
