using TravelRoutesManagement.Domain.Entities;

namespace TravelRoutesManagement.Domain.Interfaces.Repositories
{
    public interface IFlightConnectionRepository
    {
        Task Add(FlightConnection flightConnection);
        Task Update(FlightConnection flightConnection);
        Task Delete(FlightConnection flightConnection);
        Task<FlightConnection> GetById(int id);
        Task<IEnumerable<FlightConnection>> GetAll();
    }
}
