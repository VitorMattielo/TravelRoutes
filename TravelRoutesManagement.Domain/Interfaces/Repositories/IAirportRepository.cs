using TravelRoutesManagement.Domain.Entities;

namespace TravelRoutesManagement.Domain.Interfaces.Repositories
{
    public interface IAirportRepository
    {
        Task Add(Airport airport);
        Task Update(Airport airport);
        Task Delete(Airport airport);
        Task<Airport> GetById(int id);
        Task<IEnumerable<Airport>> GetAll();
    }
}
