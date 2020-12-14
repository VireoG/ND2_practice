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
    public class FiltersController : ControllerBase
    {
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public FiltersController(IEventService eventService,IMapper mapper)
        {
            this.eventService = eventService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("pagedata")]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public ActionResult GetPaggedData([FromQuery]PagedData<Event> pagedData)
        {
            var page = new PagedData<Event>();
            page = pagedData;
            if(page.Search != null)
            {
                page.Data = eventService.GetEventsBySearch(page.Search).ToList();
            }
            page.Data = eventService.GetFiltredEvents(page).ToList();
            page.TotalNumber = page.Data.Count();
            HttpContext.Response.Headers.Add("x-total-count", page.TotalNumber.ToString());
            HttpContext.Response.Headers.Add("x-current-page", page.CurrentPage.ToString());
            return Ok(mapper.Map<IEnumerable<EventResource>>(page.Data));
        }


        [HttpGet]
        [Route("autocomplete")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public ActionResult GetAutocompleteData([FromQuery]string text)
        {
            var list = eventService.GetEventsNameForAutocomplete(text);
            return Ok(list);
        } 
    }
}
