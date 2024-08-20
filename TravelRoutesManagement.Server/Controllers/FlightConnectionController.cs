using Microsoft.AspNetCore.Mvc;
using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Interfaces.Facades;

namespace TravelRoutesManagement.Api.Controllers
{
    [ApiController]
    [Route("flightConnection")]
    public class FlightConnectionController : ControllerBase
    {
        private readonly IFlightConnectionFacade _flightConnectionFacade;

        public FlightConnectionController(IFlightConnectionFacade flightConnectionFacade)
        {
            _flightConnectionFacade = flightConnectionFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlightConnection()
        {
            try
            {
                var flightConnections = await _flightConnectionFacade.GetAll();
                return Ok(flightConnections);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightConnection(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("O Id da conexão deve ser maior que zero!");

                var flightConnections = await _flightConnectionFacade.GetById(id);
                return Ok(flightConnections);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlightConnection([FromBody] DTOCreateFlightConnection airport)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _flightConnectionFacade.Create(airport);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return BadRequest(ex.InnerException.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlightConnection([FromBody] DTOUpdateFlightConnection airport)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _flightConnectionFacade.Update(airport);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlightConnection(int id)
        {
            try
            {
                await _flightConnectionFacade.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
