using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {


        [HttpGet]
        [Route("Airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = FlightStorage.FindAirports(search);
            return Ok(airports);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightRequest request)
        {
            if (!FlightStorage.IsValidSearch(request))
                return BadRequest();
            return Ok(FlightStorage.SearchFlight());
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }
    }
}