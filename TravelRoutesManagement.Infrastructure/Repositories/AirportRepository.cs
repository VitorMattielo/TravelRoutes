using Microsoft.EntityFrameworkCore;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Interfaces.Repositories;
using TravelRoutesManagement.Infrastructure.Contexts;

namespace TravelRoutesManagement.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly TravelRouteContext _context;

        public AirportRepository(TravelRouteContext context)
        {
            _context = context;
        }

        public async Task Add(Airport airport)
        {
            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Airport airport)
        {
            _context.Airports.Update(airport);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Airport airport)
        {
            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();
        }

        public async Task<Airport> GetById(int id)
        {
            return await _context.Airports.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Airport>> GetAll()
        {
            return await _context.Airports.ToListAsync();
        }
    }
}
