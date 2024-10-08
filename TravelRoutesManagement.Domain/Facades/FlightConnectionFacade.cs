﻿using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Interfaces.Facades;
using TravelRoutesManagement.Domain.Interfaces.Repositories;

namespace TravelRoutesManagement.Domain.Facades
{
    public class FlightConnectionFacade : IFlightConnectionFacade
    {
        private readonly IFlightConnectionRepository _flightConnectionRepository;

        public FlightConnectionFacade(IFlightConnectionRepository flightConnectionRepository)
        {
            _flightConnectionRepository = flightConnectionRepository;
        }

        public async Task Create(DTOCreateFlightConnection dto)
        {
            var flightConnection = new FlightConnection(dto.IdAirportOrigin, dto.IdAirportDestination, dto.Price);
            await _flightConnectionRepository.Add(flightConnection);
        }

        public async Task Update(DTOUpdateFlightConnection dto)
        {
            var flightConnection = await _flightConnectionRepository.GetById(dto.Id);

            if (flightConnection == null)
                throw new Exception("Nenhuma conexão encontrada para o Id " + dto.Id);

            flightConnection.Update(dto.IdAirportOrigin, dto.IdAirportDestination, dto.Price);
            await _flightConnectionRepository.Update(flightConnection);
        }

        public async Task Delete(int id)
        {
            var flightConnection = await _flightConnectionRepository.GetById(id);

            if (flightConnection == null)
                throw new Exception("Nenhuma conexão encontrada para o Id " + id);

            await _flightConnectionRepository.Delete(flightConnection);
        }

        public async Task<DTOGetFlightConnections> GetAll()
        {
            var flightConnections = await _flightConnectionRepository.GetAll();

            if (!flightConnections.Any())
                throw new Exception("Nenhuma conexão encontrada");

            var dtoFlightConnections = flightConnections.Select(DTOFlightConnection.Of).ToList();
            return new DTOGetFlightConnections(dtoFlightConnections);
        }

        public async Task<DTOFlightConnection> GetById(int id)
        {
            var flightConnection = await _flightConnectionRepository.GetById(id);

            if (flightConnection == null)
                throw new Exception("Nenhuma conexão encontrada para o Id " + id);

            return DTOFlightConnection.Of(flightConnection);
        }
    }
}
