using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;
using Task1_Homework.Business.Queries;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Controllers.Api.Models;

namespace Task1_Homework.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService venueService;
        private readonly IMapper mapper;

        public VenueController(IVenueService venueService, IMapper mapper)
        {
            this.venueService = venueService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("getvenues")]
        [ProducesResponseType(typeof(IEnumerable<VenueResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVenuesByCity([FromQuery]VenueQuery query)
        {
            var pagedResult = await venueService.GetVenuesByCities(query);
            return Ok(mapper.Map<IEnumerable<VenueResource>>(pagedResult.Items));
        }
    }
}
