using System.Net.Mime;
using System.Threading.Tasks;
using Eventshuffle.Api.Requests.Events;
using Eventshuffle.Application.Features.Events;
using Eventshuffle.Application.Features.Events.Commands.CreateEvent;
using Eventshuffle.Application.Features.Events.Commands.Vote;
using Eventshuffle.Application.Features.Events.Queries.GetAllEvents;
using Eventshuffle.Application.Features.Events.Queries.GetEvent;
using Eventshuffle.Application.Features.Events.Queries.GetSuitableDates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventshuffle.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// List all events.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EventListDto>> GetAll()
        {
            var eventListDto = await _mediator.Send(new GetAllEventsQuery());
            return Ok(eventListDto);
        }

        /// <summary>
        /// Show an event.
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventDto>> GetById(long id)
        {
            var eventDto = await _mediator.Send(new GetEventQuery(id));
            return Ok(eventDto);
        }

        /// <summary>
        /// Create an event.
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> Create([FromBody] CreateEventRequest request)
        {
            var command = new CreateEventCommand(request.Name, request.Dates);
            var eventCreatedDto = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetById), 
                new { version = "1.0", id = eventCreatedDto.Id }, 
                eventCreatedDto);
        }

        /// <summary>
        /// Add votes to an event.
        /// </summary>
        [HttpPost("{id:long}/vote")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventDto>> AddVotes(long id, [FromBody] VoteRequest request)
        {
            var command = new VoteCommand(id, request.Name, request.Votes);
            var eventDto = await _mediator.Send(command);
            return Ok(eventDto);
        }

        /// <summary>
        /// Show the results of an event.
        /// Responds with dates that are suitable for all participants.
        /// </summary>
        [HttpGet("{id:long}/results")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuitableDatesDto>> GetResults(long id)
        {
            var suitableDatesDto = await _mediator.Send(new GetSuitableDatesQuery(id));
            return Ok(suitableDatesDto);
        }
    }
}
