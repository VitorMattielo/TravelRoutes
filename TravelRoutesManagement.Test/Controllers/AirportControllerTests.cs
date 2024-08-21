using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRoutesManagement.Api.Controllers;
using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Interfaces.Facades;
using TravelRoutesManagement.Test.Mocks;

namespace TravelRoutesManagement.Tests
{
    [TestClass]
    public class AirportControllerTests
    {
        private AirportController _controller;
        private IAirportFacade _mockAirportFacade;

        [TestInitialize]
        public void Setup()
        {
            _mockAirportFacade = new MockAirportFacade();
            _controller = new AirportController(_mockAirportFacade);
        }

        [TestMethod]
        public async Task GetAllAirport_ReturnsOkObjectResult()
        {
            // Act
            var result = await _controller.GetAllAirport();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAirport_ReturnsOkObjectResult()
        {
            // Act
            var result = await _controller.GetAirport(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAirport_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetAirport(0);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var errorMessage = badRequestResult.Value.ToString();
            Assert.AreEqual("O Id do aeroporto deve ser maior que zero!", errorMessage);
        }

        [TestMethod]
        public async Task CreateAirport_ReturnsOkResult()
        {
            // Arrange
            var dto = new DTOCreateAirport
            {
                Name = "Nome Aeroporto",
                Acronym = "NAP"
            };

            // Act
            var result = await _controller.CreateAirport(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateAirport_ReturnsBadRequest()
        {
            // Arrange
            var dto = new DTOCreateAirport
            {
                Name = string.Empty,
                Acronym = "TEST"
            };

            _controller.ModelState.AddModelError("Name", "O nome do aeroporto é obrigatório!");
            _controller.ModelState.AddModelError("Acronym", "A sigla deve ter exatamente 3 caracteres!");

            // Act
            var result = await _controller.CreateAirport(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsNotNull(modelState);

            Assert.IsTrue(modelState.ContainsKey("Name"));
            Assert.AreEqual("O nome do aeroporto é obrigatório!", ((string[])modelState["Name"])[0]);

            Assert.IsTrue(modelState.ContainsKey("Acronym"));
            Assert.AreEqual("A sigla deve ter exatamente 3 caracteres!", ((string[])modelState["Acronym"])[0]);
        }

        [TestMethod]
        public async Task UpdateAirport_ReturnsOkResult()
        {
            // Arrange
            var dto = new DTOUpdateAirport
            {
                Id = 1,
                Name = "Atualizando Nome Aeroporto",
                Acronym = "ANA"
            };

            // Act
            var result = await _controller.UpdateAirport(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task UpdateAirport_ReturnsBadRequest()
        {
            // Arrange
            var dto = new DTOUpdateAirport
            {
                Id = 0,
                Name = string.Empty,
                Acronym = string.Empty
            };

            _controller.ModelState.AddModelError("Id", "O Id do aeroporto deve ser maior que zero!");
            _controller.ModelState.AddModelError("Name", "O nome do aeroporto é obrigatório!");
            _controller.ModelState.AddModelError("Acronym", "A sigla do aeroporto é obrigatória!");

            // Act
            var result = await _controller.UpdateAirport(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsNotNull(modelState);

            Assert.IsTrue(modelState.ContainsKey("Id"));
            Assert.AreEqual("O Id do aeroporto deve ser maior que zero!", ((string[])modelState["Id"])[0]);

            Assert.IsTrue(modelState.ContainsKey("Name"));
            Assert.AreEqual("O nome do aeroporto é obrigatório!", ((string[])modelState["Name"])[0]);

            Assert.IsTrue(modelState.ContainsKey("Acronym"));
            Assert.AreEqual("A sigla do aeroporto é obrigatória!", ((string[])modelState["Acronym"])[0]);
        }

        [TestMethod]
        public async Task DeleteAirport_ReturnsOkResult()
        {
            // Act
            var result = await _controller.DeleteAirport(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteAirport_ReturnsBadRequest()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await _controller.DeleteAirport(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var errorMessage = badRequestResult.Value.ToString();
            Assert.AreEqual("Nenhum aeroporto encontrado para o Id " + id, errorMessage);
        }
    }
}
