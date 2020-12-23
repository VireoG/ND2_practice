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
    public class EventsController : ControllerBase
    {
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public EventsController(IEventService eventService,IMapper mapper)
        {
            this.eventService = eventService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public ActionResult GetPaggedData([FromQuery]PagedData<Event> pagedData)
        {
            if(pagedData.Search != null)
            {
                pagedData.Data = eventService.GetEventsBySearch(pagedData.Search).ToList();
            }
            pagedData.Data = eventService.GetFiltredEvents(pagedData).ToList();
            pagedData.TotalNumber = pagedData.Data.Count();
            HttpContext.Response.Headers.Add("x-total-count", pagedData.TotalNumber.ToString());
            HttpContext.Response.Headers.Add("x-current-page", pagedData.CurrentPage.ToString());
            return Ok(mapper.Map<IEnumerable<EventResource>>(pagedData.Data));
        }


        [HttpGet]
        [Route("autocomplete")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public ActionResult GetAutocompleteData([FromQuery]string q)
        {
            var list = eventService.GetEventsNameForAutocomplete(q);
            return Ok(list);
        } 
    }
}
