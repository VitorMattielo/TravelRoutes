using Microsoft.AspNetCore.Mvc;
using TravelRoutesManagement.Domain.Interfaces.UseCases;

namespace TravelRoutesManagement.Api.Controllers
{
    [ApiController]
    [Route("route")]
    public class RouteController : ControllerBase
    {
        private readonly IGetCheapestRouteUseCase _getCheapestRouteUseCase;

        public RouteController(IGetCheapestRouteUseCase getCheapestRouteUseCase)
        {
            _getCheapestRouteUseCase = getCheapestRouteUseCase;
        }

        [HttpGet("cheapest-route")]
        public async Task<IActionResult> GetCheapestRoute([FromQuery] int idOrigin, int idDestination)
        {
            try
            {
                if (idOrigin == 0 || idDestination == 0)
                    return BadRequest("A origem e destino devem ser ambos informados para consultar a rota mais barata!");

                var cheapestRoute = await _getCheapestRouteUseCase.GetCheapestRoute(idOrigin, idDestination);
                return Ok(cheapestRoute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
