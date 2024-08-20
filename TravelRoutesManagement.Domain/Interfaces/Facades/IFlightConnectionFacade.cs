using TravelRoutesManagement.Domain.DTOs;

namespace TravelRoutesManagement.Domain.Interfaces.Facades
{
    public interface IFlightConnectionFacade
    {
        Task Create(DTOCreateFlightConnection dto);
        Task Update(DTOUpdateFlightConnection dto);
        Task Delete(int id);
        Task<DTOGetFlightConnections> GetAll();
        Task<DTOFlightConnection> GetById(int id);
    }
}
