using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Interfaces.Facades;
using TravelRoutesManagement.Domain.Interfaces.Repositories;

namespace TravelRoutesManagement.Domain.Facades
{
    public class AirportFacade : IAirportFacade
    {
        private readonly IAirportRepository _airportRepository;

        public AirportFacade(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task Create(DTOCreateAirport dto)
        {
            var airport = new Airport(dto.Name, dto.Acronym);
            await _airportRepository.Add(airport);
        }

        public async Task Update(DTOUpdateAirport dto)
        {
            var airport = await _airportRepository.GetById(dto.Id);

            if (airport == null)
                throw new Exception("Nenhum aeroporto encontrado para o Id " + dto.Id);

            airport.Update(dto.Name, dto.Acronym);
            await _airportRepository.Update(airport);
        }

        public async Task Delete(int id)
        {
            var airport = await _airportRepository.GetById(id);

            if (airport == null)
                throw new Exception("Nenhum aeroporto encontrado para o Id " + id);

            await _airportRepository.Delete(airport);
        }

        public async Task<DTOGetAirports> GetAll()
        {
            var airports = await _airportRepository.GetAll();

            if (!airports.Any())
                throw new Exception("Nenhum aeroporto encontrado");

            var dtoAirports = airports.Select(DTOAirport.Of).ToList();
            return new DTOGetAirports(dtoAirports);
        }

        public async Task<DTOAirport> GetById(int id)
        {
            var airport = await _airportRepository.GetById(id);

            if (airport == null)
                throw new Exception("Nenhum aeroporto encontrado para o Id " + id);

            return DTOAirport.Of(airport);
        }
    }
}
