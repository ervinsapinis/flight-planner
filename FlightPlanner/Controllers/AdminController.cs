using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [EnableCors]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly FlightPlannerDbContext _context;
        private static readonly object _lock = new();

        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Flights/{id}")]
        [Authorize]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        [Authorize]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lock)
            {
                FlightStorage.DeleteFlight(id);
                return Ok();
            }
        }

        [HttpPut]
        [Route("Flights")]
        [Authorize]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            lock (_lock)
            {
                if (!FlightStorage.IsValid(request))
                    return BadRequest();

                if (FlightStorage.Exists(request))
                    return Conflict();

                return Created("", FlightStorage.AddFlight(request));
            }
        }
    }
}