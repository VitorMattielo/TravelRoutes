using Microsoft.EntityFrameworkCore;
using TravelRoutesManagement.Domain.Entities;

namespace TravelRoutesManagement.Infrastructure.Contexts
{
    public class TravelRouteContext : DbContext
    {
        public TravelRouteContext(DbContextOptions<TravelRouteContext> options) : base(options) { }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<FlightConnection> FlightConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FlightConnection>()
                .HasIndex(x => new { x.AirportOriginId, x.AirportDestinationId })
                .IsUnique();
        }
    }
}
