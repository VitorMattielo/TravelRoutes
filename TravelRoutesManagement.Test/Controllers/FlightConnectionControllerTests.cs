using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRoutesManagement.Api.Controllers;
using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Interfaces.Facades;
using TravelRoutesManagement.Test.Mocks;

namespace TravelRoutesManagement.Tests
{
    [TestClass]
    public class FlightConnectionControllerTests
    {
        private FlightConnectionController _controller;
        private IFlightConnectionFacade _mockFlightConnectionFacade;

        [TestInitialize]
        public void Setup()
        {
            _mockFlightConnectionFacade = new MockFlightConnectionFacade();
            _controller = new FlightConnectionController(_mockFlightConnectionFacade);
        }

        [TestMethod]
        public async Task GetAllFlightConnection_ReturnsOkObjectResult()
        {
            // Act
            var result = await _controller.GetAllFlightConnection();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetByIdFlightConnection_ReturnsOkObjectResult()
        {
            // Act
            var result = await _controller.GetFlightConnection(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetByIdFlightConnection_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetFlightConnection(0);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var errorMessage = badRequestResult.Value.ToString();
            Assert.AreEqual("O Id da conexão deve ser maior que zero!", errorMessage);
        }

        [TestMethod]
        public async Task CreateFlightConnection_ReturnsOkResult()
        {
            // Arrange
            var dto = new DTOCreateFlightConnection
            {
                IdAirportOrigin = 1,
                IdAirportDestination = 2,
                Price = 20
            };

            // Act
            var result = await _controller.CreateFlightConnection(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateFlightConnection_ReturnsBadRequest()
        {
            // Arrange
            var dto = new DTOCreateFlightConnection
            {
                IdAirportOrigin = 0,
                IdAirportDestination = 0,
                Price = 0
            };

            _controller.ModelState.AddModelError("IdAirportOrigin", "O Id do aeroporto de origem deve ser maior que zero!");
            _controller.ModelState.AddModelError("IdAirportDestination", "O Id do aeroporto de destino deve ser maior que zero!");
            _controller.ModelState.AddModelError("Price", "O preço deve ser no mínimo 0,01!");

            // Act
            var result = await _controller.CreateFlightConnection(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsNotNull(modelState);

            Assert.IsTrue(modelState.ContainsKey("IdAirportOrigin"));
            Assert.AreEqual("O Id do aeroporto de origem deve ser maior que zero!", ((string[])modelState["IdAirportOrigin"])[0]);

            Assert.IsTrue(modelState.ContainsKey("IdAirportDestination"));
            Assert.AreEqual("O Id do aeroporto de destino deve ser maior que zero!", ((string[])modelState["IdAirportDestination"])[0]);

            Assert.IsTrue(modelState.ContainsKey("Price"));
            Assert.AreEqual("O preço deve ser no mínimo 0,01!", ((string[])modelState["Price"])[0]);
        }

        [TestMethod]
        public async Task UpdateFlightConnection_ReturnsOkResult()
        {
            // Arrange
            var dto = new DTOUpdateFlightConnection
            {
                Id = 1,
                IdAirportOrigin = 1,
                IdAirportDestination = 2,
                Price = 20
            };

            // Act
            var result = await _controller.UpdateFlightConnection(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task UpdateFlightConnection_ReturnsBadRequest()
        {
            // Arrange
            var dto = new DTOUpdateFlightConnection
            {
                Id = 0,
                IdAirportOrigin = 0,
                IdAirportDestination = 0,
                Price = 0
            };

            _controller.ModelState.AddModelError("Id", "O Id da conexão deve ser maior que zero!");
            _controller.ModelState.AddModelError("IdAirportOrigin", "O Id do aeroporto de origem deve ser maior que zero!");
            _controller.ModelState.AddModelError("IdAirportDestination", "O Id do aeroporto de destino deve ser maior que zero!");
            _controller.ModelState.AddModelError("Price", "O preço deve ser no mínimo 0,01!");

            // Act
            var result = await _controller.CreateFlightConnection(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsNotNull(modelState);

            Assert.IsTrue(modelState.ContainsKey("Id"));
            Assert.AreEqual("O Id da conexão deve ser maior que zero!", ((string[])modelState["Id"])[0]);

            Assert.IsTrue(modelState.ContainsKey("IdAirportOrigin"));
            Assert.AreEqual("O Id do aeroporto de origem deve ser maior que zero!", ((string[])modelState["IdAirportOrigin"])[0]);

            Assert.IsTrue(modelState.ContainsKey("IdAirportDestination"));
            Assert.AreEqual("O Id do aeroporto de destino deve ser maior que zero!", ((string[])modelState["IdAirportDestination"])[0]);

            Assert.IsTrue(modelState.ContainsKey("Price"));
            Assert.AreEqual("O preço deve ser no mínimo 0,01!", ((string[])modelState["Price"])[0]);
        }

        [TestMethod]
        public async Task DeleteFlightConnection_ReturnsOkResult()
        {
            // Act
            var result = await _controller.DeleteFlightConnection(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteFlightConnection_ReturnsBadRequest()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await _controller.DeleteFlightConnection(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);

            var errorMessage = badRequestResult.Value.ToString();
            Assert.AreEqual("Nenhuma conexão encontrada para o Id " + id, errorMessage);
        }
    }
}
