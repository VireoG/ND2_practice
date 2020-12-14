using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Task1_Homework.Business;
using Task1_Homework.Business.Queries;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Controllers.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Task1_Homework.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaginationController : ControllerBase
    {
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public PaginationController(IEventService eventService,IMapper mapper)
        {
            this.eventService = eventService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("pagedata")]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public ActionResult GetPaggedData([FromQuery]PagedData<Event> pagedData)
        {
            pagedData.Data = eventService.GetFiltredEvents(pagedData).Result.ToList();
            pagedData.TotalNumber = pagedData.Data.Count();
            HttpContext.Response.Headers.Add("x-total-count", pagedData.TotalNumber.ToString());
            HttpContext.Response.Headers.Add("x-current-page", pagedData.CurrentPage.ToString());
            return Ok(mapper.Map<IEnumerable<EventResource>>(pagedData.Data));
        }
    }
}
