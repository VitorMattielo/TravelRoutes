using Microsoft.AspNetCore.Mvc;
using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Interfaces.Facades;

namespace TravelRoutesManagement.Api.Controllers
{
    [ApiController]
    [Route("airport")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportFacade _airportFacade;

        public AirportController(IAirportFacade airportFacade)
        {
            _airportFacade = airportFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAirport()
        {
            try
            {
                var airports = await _airportFacade.GetAll();
                return Ok(airports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirport(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("O Id do aeroporto deve ser maior que zero!");

                var airports = await _airportFacade.GetById(id);
                return Ok(airports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAirport([FromBody] DTOCreateAirport airport)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _airportFacade.Create(airport);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAirport([FromBody] DTOUpdateAirport airport)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _airportFacade.Update(airport);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            try
            {
                await _airportFacade.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
