using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Interfaces.Repositories;
using TravelRoutesManagement.Domain.Interfaces.UseCases;
using TravelRoutesManagement.Domain.UseCases;
using TravelRoutesManagement.Infrastructure.Contexts;
using TravelRoutesManagement.Infrastructure.Repositories;

namespace TravelRoutesManagement.Test.UseCases
{
    [TestClass]
    public class GetCheapestRouteUseCaseTests
    {
        private TravelRouteContext _context;
        private IFlightConnectionRepository _flightConnectionRepository;
        private IGetCheapestRouteUseCase _useCase;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TravelRouteContext>()
                .UseInMemoryDatabase(databaseName: "GetCheapestRouteUseCaseDatabase")
                .Options;

            _context = new TravelRouteContext(options);
            _flightConnectionRepository = new FlightConnectionRepository(_context);
            _useCase = new GetCheapestRouteUseCase(_flightConnectionRepository);
        }

        [TestMethod]
        public async Task GetCheapestRoute_SholdGetCheapestRoute()
        {
            // Arrange
            await ClearData();

            var airports = new List<Airport> {
                new Airport("Aeroporto Internacional de Guarulhos – Cumbica", "GRU"),
                new Airport("Aeroporto Internacional Teniente Luis Candelaria", "BRC"),
                new Airport("Aeroporto Internacional de Orlando", "ORL"),
                new Airport("Aeroporto Internacional Arturo Merino Benítez", "SCL"),
                new Airport("Aeroporto de Paris-Charles de Gaulle", "CDG")
            };
            _context.Airports.AddRange(airports);
            _context.SaveChanges();

            var gru = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("GRU"));
            var brc = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("BRC"));
            var orl = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("ORL"));
            var scl = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("SCL"));
            var cdg = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("CDG"));

            var flightConnections = new List<FlightConnection> {
                new FlightConnection(gru.Id, brc.Id, 10),
                new FlightConnection(brc.Id, scl.Id, 5),
                new FlightConnection(gru.Id, cdg.Id, 75),
                new FlightConnection(gru.Id, scl.Id, 20),
                new FlightConnection(gru.Id, orl.Id, 56),
                new FlightConnection(orl.Id, cdg.Id, 5),
                new FlightConnection(scl.Id, orl.Id, 20)
            };
            _context.FlightConnections.AddRange(flightConnections);
            await _context.SaveChangesAsync();

            // Act
            var result = await _useCase.GetCheapestRoute(gru.Id, cdg.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("A rota mais barata é a seguinte:\nGRU - BRC - SCL - ORL - CDG com o valor total de R$ 40,00", result);
        }

        [TestMethod]
        public async Task GetCheapestRoute_SholdNotGetAnyRoute()
        {
            // Arrange
            await ClearData();

            var airports = new List<Airport> {
                new Airport("Aeroporto Internacional de Guarulhos – Cumbica", "GRU"),
                new Airport("Aeroporto Internacional Teniente Luis Candelaria", "BRC"),
                new Airport("Aeroporto Internacional de Orlando", "ORL"),
                new Airport("Aeroporto Internacional Arturo Merino Benítez", "SCL"),
                new Airport("Aeroporto de Paris-Charles de Gaulle", "CDG")
            };
            _context.Airports.AddRange(airports);
            _context.SaveChanges();

            var gru = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("GRU"));
            var brc = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("BRC"));
            var orl = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("ORL"));
            var scl = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("SCL"));
            var cdg = _context.Airports.FirstOrDefault(x => x.Acronym.Equals("CDG"));

            var flightConnections = new List<FlightConnection> {
                new FlightConnection(gru.Id, brc.Id, 10),
                new FlightConnection(brc.Id, scl.Id, 5),
                new FlightConnection(gru.Id, cdg.Id, 75),
                new FlightConnection(gru.Id, scl.Id, 20),
                new FlightConnection(gru.Id, orl.Id, 56),
                new FlightConnection(orl.Id, cdg.Id, 5),
                new FlightConnection(scl.Id, orl.Id, 20)
            };
            _context.FlightConnections.AddRange(flightConnections);
            await _context.SaveChangesAsync();

            // Act
            var result = await _useCase.GetCheapestRoute(cdg.Id, gru.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Não foram encontradas nenhuma rota até o destino!", result);
        }

        private async Task ClearData()
        {
            var removeFlightConnections = _context.FlightConnections.ToList();
            _context.FlightConnections.RemoveRange(removeFlightConnections);

            var removeAirports = _context.Airports.ToList();
            _context.Airports.RemoveRange(removeAirports);
            await _context.SaveChangesAsync();
        }
    }
}
