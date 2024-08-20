using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Interfaces.Repositories;
using TravelRoutesManagement.Domain.Interfaces.UseCases;

namespace TravelRoutesManagement.Domain.UseCases
{
    public class GetCheapestRouteUseCase : IGetCheapestRouteUseCase
    {
        private readonly IFlightConnectionRepository _flightConnectionRepository;
        private Dictionary<int, List<FlightConnection>> _auxiliarFlightConnections = new Dictionary<int, List<FlightConnection>>();

        public GetCheapestRouteUseCase(IFlightConnectionRepository flightConnectionRepository)
        {
            _flightConnectionRepository = flightConnectionRepository;
        }

        public async Task<string> GetCheapestRoute(int idOrigin, int idDestination)
        {
            var flightConnections = await _flightConnectionRepository.GetAll();

            PopulateFlightConnections(flightConnections);

            var routes = FindAllPossibleRoutes(idOrigin, idDestination);

            return DetailCheapestRoute(routes);
        }

        private void PopulateFlightConnections(IEnumerable<FlightConnection> flightConnections)
        {
            _auxiliarFlightConnections = new Dictionary<int, List<FlightConnection>>();

            foreach (var connection in flightConnections)
            {
                if (!_auxiliarFlightConnections.ContainsKey(connection.AirportOriginId))
                    _auxiliarFlightConnections[connection.AirportOriginId] = new List<FlightConnection>();

                _auxiliarFlightConnections[connection.AirportOriginId].Add(connection);
            }
        }

        public List<DTOCheapestRoute> FindAllPossibleRoutes(int idOrigin, int idDestination)
        {
            var visited = new HashSet<int>();
            var currentRoute = new DTOCheapestRoute();
            var routes = new List<DTOCheapestRoute>();

            SearchNextConections(idOrigin, idDestination, visited, currentRoute, routes);

            return routes;
        }

        private void SearchNextConections(int current, int targetDestination, HashSet<int> visited, DTOCheapestRoute currentRoute, List<DTOCheapestRoute> routes)
        {
            visited.Add(current);

            if (current == targetDestination)
            {
                var routeCopy = new DTOCheapestRoute { Connections = new List<DTOFlightConnection>(currentRoute.Connections) };
                routes.Add(routeCopy);
            }
            else if (_auxiliarFlightConnections.ContainsKey(current))
            {
                foreach (var connection in _auxiliarFlightConnections[current])
                {
                    if (!visited.Contains(connection.AirportDestinationId))
                    {
                        currentRoute.Connections.Add(DTOFlightConnection.Of(connection));
                        SearchNextConections(connection.AirportDestinationId, targetDestination, visited, currentRoute, routes);
                        currentRoute.Connections.RemoveAt(currentRoute.Connections.Count - 1);
                    }
                }
            }
            visited.Remove(current);
        }

        private string DetailCheapestRoute(List<DTOCheapestRoute> routes)
        {
            if (!routes.Any())
                return "Não foram encontradas nenhuma rota até o destino!";

            var route = routes
                .OrderBy(x => x.TotalPrice)
                .ThenBy(x => x.Connections.Count)
                .FirstOrDefault();

            string result = string.Concat("A rota mais barata é a seguinte:\n" +
                                          string.Join(" - ", route.Connections.Select(fc => fc.AcronymAirportOrigin)), 
                                          " - ", 
                                          route.Connections.Last().AcronymAirportDestination,
                                          " com o valor total de " + route.TotalPrice.ToString("C2"));

            return result;
        }
    }
}
