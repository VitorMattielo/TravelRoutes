using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Facades;
using TravelRoutesManagement.Domain.Interfaces.Facades;
using TravelRoutesManagement.Domain.Interfaces.Repositories;
using TravelRoutesManagement.Infrastructure.Contexts;
using TravelRoutesManagement.Infrastructure.Repositories;

namespace TravelRoutesManagement.Test.Facades
{
    [TestClass]
    public class AirportFacadeTests
    {
        private TravelRouteContext _context;
        private IAirportRepository _airportRepository;
        private IAirportFacade _facade;
        

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TravelRouteContext>()
                .UseInMemoryDatabase(databaseName: "AirportFacadeDatabase")
                .Options;

            _context = new TravelRouteContext(options);
            _airportRepository = new AirportRepository(_context);
            _facade = new AirportFacade(_airportRepository);
        }

        [TestMethod]
        public async Task Create_ShouldAddAirport()
        {
            // Arrange
            await ClearData();

            var dto = new DTOCreateAirport
            {
                Name = "Nome Aeroporto",
                Acronym = "NAP"
            };

            // Act
            await _facade.Create(dto);

            var airport = (await _airportRepository.GetAll()).FirstOrDefault(x => x.Name.Equals(dto.Name) && x.Acronym.Equals(dto.Acronym));

            // Assert
            Assert.IsNotNull(airport);
            Assert.AreEqual(dto.Name, airport.Name);
            Assert.AreEqual(dto.Acronym, airport.Acronym);
        }

        [TestMethod]
        public async Task Update_ShouldUpdateAirport()
        {
            // Arrange
            await ClearData();

            var createdAirport = new Airport("Nome Aeroporto", "NAP");
            _context.Airports.Add(createdAirport);
            await _context.SaveChangesAsync();

            var dto = new DTOUpdateAirport
            {
                Id = createdAirport.Id,
                Name = "Nome Aeroporto Atualizado",
                Acronym = "NAA"
            };

            // Act
            await _facade.Update(dto);

            var airport = await _airportRepository.GetById(createdAirport.Id);

            // Assert
            Assert.IsNotNull(airport);
            Assert.AreEqual(dto.Name, airport.Name);
            Assert.AreEqual(dto.Acronym, airport.Acronym);
        }

        [TestMethod]
        public async Task Update_ShouldThrowException()
        {
            // Arrange
            await ClearData();

            var dto = new DTOUpdateAirport
            {
                Id = 1,
                Name = "Nome Aeroporto Atualizado",
                Acronym = "NAA"
            };

            // Act - Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _facade.Update(dto));
        }

        [TestMethod]
        public async Task Delete_ShouldDeleteAirport()
        {
            // Arrange
            await ClearData();

            var createdAirport = new Airport("Nome Aeroporto", "NAP");
            _context.Airports.Add(createdAirport);
            await _context.SaveChangesAsync();

            // Act
            await _facade.Delete(createdAirport.Id);

            // Assert
            Assert.IsFalse(_context.Airports.Any());
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
        public async Task GetById_ShouldReturnAirport()
        {
            // Arrange
            await ClearData();

            var createdAirport = new Airport("Nome Aeroporto", "NAP");
            _context.Airports.Add(createdAirport);
            await _context.SaveChangesAsync();

            // Act
            var result = await _facade.GetById(createdAirport.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, createdAirport.Name);
            Assert.AreEqual(result.Acronym, createdAirport.Acronym);
        }

        [TestMethod]
        public async Task GetById_ThrowsException()
        {
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _facade.GetById(99));
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAirports()
        {
            // Arrange
            await ClearData();

            var airport1 = new Airport("Nome Aeroporto 1", "NA1");
            var airport2 = new Airport("Nome Aeroporto 2", "NA2");
            _context.Airports.AddRange(airport1, airport2);
            _context.SaveChanges();

            // Act
            var result = await _facade.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Airports.Count);
            Assert.IsTrue(result.Airports.Any(a => a.Acronym == "NA1"));
            Assert.IsTrue(result.Airports.Any(a => a.Name == "Nome Aeroporto 2"));
        }

        [TestMethod]
        public async Task GetAll_ShouldThrowException()
        {
            // Arrange
            await ClearData();

            // Act - Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _facade.GetAll());
        }

        private async Task ClearData()
        {
            var removeAll = _context.Airports.ToList();
            _context.Airports.RemoveRange(removeAll);
            await _context.SaveChangesAsync();
        }
    }
}
