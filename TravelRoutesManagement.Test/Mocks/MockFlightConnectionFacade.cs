using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Interfaces.Facades;

namespace TravelRoutesManagement.Test.Mocks
{
    public class MockFlightConnectionFacade : IFlightConnectionFacade
    {
        public Task<DTOGetFlightConnections> GetAll()
        {
            return Task.FromResult(new DTOGetFlightConnections(
                new List<DTOFlightConnection>
                {
                        new DTOFlightConnection { Id = 1, IdAirportOrigin = 1, IdAirportDestination = 2, Price = 15 },
                        new DTOFlightConnection { Id = 2, IdAirportOrigin = 1, IdAirportDestination = 3, Price = 30 },
                        new DTOFlightConnection { Id = 3, IdAirportOrigin = 1, IdAirportDestination = 4, Price = 5 },
                        new DTOFlightConnection { Id = 4, IdAirportOrigin = 1, IdAirportDestination = 5, Price = 70 }
                }));
        }

        public Task<DTOFlightConnection> GetById(int id)
        {
            return Task.FromResult(new DTOFlightConnection { Id = 1, IdAirportOrigin = 1, IdAirportDestination = 2, Price = 15 });
        }

        public Task Create(DTOCreateFlightConnection flightConnection)
        {
            return Task.CompletedTask;
        }

        public Task Update(DTOUpdateFlightConnection flightConnection)
        {
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            if (id == 0)
                throw new Exception("Nenhuma conexão encontrada para o Id " + id);
            return Task.CompletedTask;
        }
    }
}
