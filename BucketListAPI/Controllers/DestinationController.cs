using BucketListServices;
using BucketListModels;
using Microsoft.AspNetCore.Mvc;

namespace BucketList.API.Controllers
{
    [ApiController]
    [Route("api/destination")]
    public class DestinationController : ControllerBase
    {
        private readonly DestinationGetServices _destinationGetServices;

        public DestinationController()
        {
            _destinationGetServices = new DestinationGetServices(); 
        }

        [HttpGet]
        public IActionResult GetAllDestinations()
        {
            var destinations = _destinationGetServices.GetAllDestinations();
            return Ok(destinations);
        }

        [HttpGet("{name}")]
        public IActionResult GetDestinationByName(string name)
        {
            var destination = _destinationGetServices.GetDestinationByName(name);
            if (destination == null)
                return NotFound();

            return Ok(destination);
        }

        [HttpPost]
        public IActionResult AddNewDestination([FromBody] Destination newDestination)
        {
            if (newDestination == null)
                return BadRequest("Destination cannot be null");

            _destinationGetServices.AddNewDestination(newDestination);
            return CreatedAtAction(nameof(GetDestinationByName), new { name = newDestination.Name }, newDestination);
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteDestination(string name)
        {
            var destination = _destinationGetServices.GetDestinationByName(name);
            if (destination == null)
                return NotFound();

            _destinationGetServices.DeleteDestination(name);
            return NoContent();
        }

        [HttpPatch]
        public IActionResult UpdateDestination([FromBody] Destination updatedDestination)
        {
            if (updatedDestination == null)
                return BadRequest("Destination cannot be null");

            var existingDestination = _destinationGetServices.GetDestinationByName(updatedDestination.Name);
            if (existingDestination == null)
                return NotFound();

            _destinationGetServices.UpdateDestination(updatedDestination);
            return NoContent();
        }
    }
}
