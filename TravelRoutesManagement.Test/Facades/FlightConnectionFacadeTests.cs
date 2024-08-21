using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Facades;
using TravelRoutesManagement.Domain.Interfaces.Facades;
using TravelRoutesManagement.Domain.Interfaces.Repositories;
using TravelRoutesManagement.Infrastructure.Contexts;
using TravelRoutesManagement.Infrastructure.Repositories;

namespace TravelRoutesManagement.Tests
{
    [TestClass]
    public class FlightConnectionFacadeTests
    {
        private TravelRouteContext _context;
        private IFlightConnectionRepository _flightConnectionRepository;
        private IFlightConnectionFacade _facade;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TravelRouteContext>()
                .UseInMemoryDatabase(databaseName: "FlightConnectionFacadeDatabase")
                .Options;

            _context = new TravelRouteContext(options);
            _flightConnectionRepository = new FlightConnectionRepository(_context);
            _facade = new FlightConnectionFacade(_flightConnectionRepository);

            PopulateData();
        }

        [TestMethod]
        public async Task Create_ShouldAddFlightConnection()
        {
            // Arrange
            var dto = new DTOCreateFlightConnection
            {
                IdAirportOrigin = 1,
                IdAirportDestination = 2,
                Price = 15
            };

            // Act
            await _facade.Create(dto);

            var flightConnection = (await _flightConnectionRepository.GetAll())
                .FirstOrDefault(x => x.AirportOriginId == dto.IdAirportOrigin && x.AirportDestinationId == dto.IdAirportDestination);

            // Assert
            Assert.IsNotNull(flightConnection);
            Assert.AreEqual(dto.IdAirportOrigin, flightConnection.AirportOriginId);
            Assert.AreEqual(dto.IdAirportDestination, flightConnection.AirportDestinationId);
            Assert.AreEqual(dto.Price, flightConnection.Price);
            Assert.IsNotNull(flightConnection.AirportOrigin);
            Assert.IsNotNull(flightConnection.AirportDestination);
            Assert.AreEqual("Nome Aeroporto 1", flightConnection.AirportOrigin.Name);
            Assert.AreEqual("Nome Aeroporto 2", flightConnection.AirportDestination.Name);
        }

        [TestMethod]
        public async Task Update_ShouldUpdateFlightConnection()
        {
            // Arrange
            await ClearData();

            var createdFlightConnection = new FlightConnection(1, 2, 25);
            _context.FlightConnections.Add(createdFlightConnection);
            await _context.SaveChangesAsync();

            var dto = new DTOUpdateFlightConnection
            {
                Id = createdFlightConnection.Id,
                IdAirportOrigin = 1,
                IdAirportDestination = 2,
                Price = 25
            };

            // Act
            await _facade.Update(dto);

            var flightConnection = (await _flightConnectionRepository.GetAll())
                .FirstOrDefault(x => x.AirportOriginId == dto.IdAirportOrigin && x.AirportDestinationId == dto.IdAirportDestination);

            // Assert
            Assert.IsNotNull(flightConnection);
            Assert.AreEqual(dto.IdAirportOrigin, flightConnection.AirportOriginId);
            Assert.AreEqual(dto.IdAirportDestination, flightConnection.AirportDestinationId);
            Assert.AreEqual(dto.Price, flightConnection.Price);
            Assert.IsNotNull(flightConnection.AirportOrigin);
            Assert.IsNotNull(flightConnection.AirportDestination);
            Assert.AreEqual("Nome Aeroporto 1", flightConnection.AirportOrigin.Name);
            Assert.AreEqual("Nome Aeroporto 2", flightConnection.AirportDestination.Name);
        }

        [TestMethod]
        public async Task Update_ShouldThrowException()
        {
            // Arrange
            await ClearData();

            var dto = new DTOUpdateFlightConnection
            {
                Id = 1,
                IdAirportOrigin = 1,
                IdAirportDestination = 2,
                Price = 25
            };

            // Act - Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _facade.Update(dto));
        }

        [TestMethod]
        public async Task Delete_ShouldUpdateFlightConnection()
        {
            // Arrange
            await ClearData();

            var createdFlightConnection = new FlightConnection(1, 2, 25);
            _context.FlightConnections.Add(createdFlightConnection);
            await _context.SaveChangesAsync();

            // Act
            await _facade.Delete(createdFlightConnection.Id);

            // Assert
            Assert.IsFalse(_context.FlightConnections.Any());
        }

        [TestMethod]
        public async Task Delete_ShouldThrowException()
        {
            // Arrange
            await ClearData();

            // Act - Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _facade.Delete(1));
        }

        [TestMethod]
        public async Task GetById_ShouldReturnFlightConnection()
        {
            // Arrange
            await ClearData();

            var flightConnection = new FlightConnection(1, 2, 150);
            _context.FlightConnections.Add(flightConnection);
            await _context.SaveChangesAsync();

            // Act
            var result = await _facade.GetById(flightConnection.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(flightConnection.AirportOriginId, result.IdAirportOrigin);
            Assert.AreEqual(flightConnection.AirportDestinationId, result.IdAirportDestination);
            Assert.AreEqual(flightConnection.AirportOrigin.Acronym, result.AcronymAirportOrigin);
            Assert.AreEqual(flightConnection.AirportDestination.Acronym, result.AcronymAirportDestination);
        }

        [TestMethod]
        public async Task GetById_ShouldThrowException()
        {
            // Arrange
            await ClearData();

            // Act - Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _facade.GetById(1));
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnFlightConnections()
        {
            // Arrange
            await ClearData();

            var flightConnection1 = new FlightConnection(1, 2, 150);
            var flightConnection2 = new FlightConnection(1, 2, 200);
            _context.FlightConnections.AddRange(flightConnection1, flightConnection2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _facade.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.FlightConnections.Count);
            Assert.IsTrue(result.FlightConnections.All(fc => fc.AcronymAirportOrigin == "NA1"));
            Assert.IsTrue(result.FlightConnections.All(fc => fc.AcronymAirportDestination == "NA2"));
        }

        [TestMethod]
        public async Task GetAll_ShouldThrowException()
        {
            // Arrange
            await ClearData();

            // Act - Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _facade.GetAll());
        }

        private void PopulateData()
        {
            var airport1 = new Airport("Nome Aeroporto 1", "NA1");
            var airport2 = new Airport("Nome Aeroporto 2", "NA2");
            _context.Airports.AddRange(airport1, airport2);
            _context.SaveChanges();
        }

        private async Task ClearData()
        {
            var removeAll = _context.FlightConnections.ToList();
            _context.FlightConnections.RemoveRange(removeAll);
            await _context.SaveChangesAsync();
        }
    }
}
