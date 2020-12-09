using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Queries;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Controllers.Api.Models;

namespace Task1_Homework.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ResaleContext context;
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public EventsController(ResaleContext context, IEventService eventService, IMapper mapper)
        {
            this.context = context;
            this.eventService = eventService;
            this.mapper = mapper;
        }

        //GET: api/v1/Events
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Event>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await eventService.GetEvents());
        }

        // GET: api/v1/Events/5
        [HttpGet("{id}.{format?}")]
        public ActionResult<Event> GetEvent(int id)
        {
            var @event = eventService.GetEventById(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        [HttpGet]
        [Route("getfiltred")]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFiltredEvents([FromQuery]EventQuery query)
        {
            var pagedResult = await eventService.GetEvents(query);
            HttpContext.Response.Headers.Add("x-total-count", pagedResult.TotalCount.ToString());

            return Ok(mapper.Map<IEnumerable<EventResource>>(pagedResult.Items));
        }

        // PUT: api/v1/Events/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            eventService.GetEntry(@event);          

            try
            {
                await eventService.Save(@event);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eventService.EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/v1/Events
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            await eventService.Save(@event);

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/v1/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
            }

            await eventService.Delete(@event);

            return @event;
        }
    }
}
