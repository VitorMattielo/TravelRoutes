using TravelRoutesManagement.Domain.DTOs;

namespace TravelRoutesManagement.Domain.Interfaces.Facades
{
    public interface IAirportFacade
    {
        Task Create(DTOCreateAirport dto);
        Task Update(DTOUpdateAirport dto);
        Task Delete(int id);
        Task<DTOGetAirports> GetAll();
        Task<DTOAirport> GetById(int id);
    }
}
