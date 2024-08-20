using Microsoft.EntityFrameworkCore;
using TravelRoutesManagement.Domain.Entities;
using TravelRoutesManagement.Domain.Interfaces.Repositories;
using TravelRoutesManagement.Infrastructure.Contexts;

namespace TravelRoutesManagement.Infrastructure.Repositories
{
    public class FlightConnectionRepository : IFlightConnectionRepository
    {
        private readonly TravelRouteContext _context;

        public FlightConnectionRepository(TravelRouteContext context)
        {
            _context = context;
        }

        public async Task Add(FlightConnection flightConnection)
        {
            _context.FlightConnections.Add(flightConnection);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FlightConnection flightConnection)
        {
            _context.FlightConnections.Update(flightConnection);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(FlightConnection flightConnection)
        {
            _context.FlightConnections.Remove(flightConnection);
            await _context.SaveChangesAsync();
        }

        public async Task<FlightConnection> GetById(int id)
        {
            return await _context.FlightConnections
                .Include(x => x.AirportOrigin)
                .Include(x => x.AirportDestination)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<FlightConnection>> GetAll()
        {
            return await _context.FlightConnections
                .Include(x => x.AirportOrigin)
                .Include(x => x.AirportDestination)
                .ToListAsync();
        }
    }
}
