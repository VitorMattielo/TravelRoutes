using TravelRoutesManagement.Domain.DTOs;
using TravelRoutesManagement.Domain.Interfaces.Facades;

namespace TravelRoutesManagement.Test.Mocks
{
    public class MockAirportFacade : IAirportFacade
    {
        public Task<DTOGetAirports> GetAll()
        {
            return Task.FromResult(new DTOGetAirports(
                new List<DTOAirport>
                {
                        new DTOAirport { Id = 1, Name = "Nome Aeroporto 1", Acronym = "NA1" },
                        new DTOAirport { Id = 2, Name = "Nome Aeroporto 2", Acronym = "NA2" },
                        new DTOAirport { Id = 3, Name = "Nome Aeroporto 3", Acronym = "NA3" },
                        new DTOAirport { Id = 4, Name = "Nome Aeroporto 4", Acronym = "NA4" }
                }));
        }

        public Task<DTOAirport> GetById(int id)
        {
            return Task.FromResult(new DTOAirport { Id = 1, Name = "Nome Aeroporto", Acronym = "NAP" });
        }

        public Task Create(DTOCreateAirport dto)
        {
            return Task.CompletedTask;
        }

        public Task Update(DTOUpdateAirport dto)
        {
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            if (id == 0)
                throw new Exception("Nenhum aeroporto encontrado para o Id " + id);
            return Task.CompletedTask;
        }
    }
}
