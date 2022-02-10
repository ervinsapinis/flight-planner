using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Route("Flights/{id}")]
        [Authorize]
        public IActionResult GetFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        [Authorize]
        public IActionResult DeleteFlights(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }

        [HttpPut]
        [Route("Flights")]
        [Authorize]
        public IActionResult AddFlights(AddFlightRequest request)
        {
            if (!FlightStorage.IsValid(request))
                return BadRequest();

            if (FlightStorage.Exists(request))
                return Conflict();

            return Created("", FlightStorage.AddFlight(request));
        }
    }
}