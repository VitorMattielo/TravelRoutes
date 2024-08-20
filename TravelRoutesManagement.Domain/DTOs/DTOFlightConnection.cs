using TravelRoutesManagement.Domain.Entities;

namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOFlightConnection
    {
        public int Id { get; set; }
        public int IdAirportOrigin { get; set; }
        public string AcronymAirportOrigin { get; set; }
        public int IdAirportDestination { get; set; }
        public string AcronymAirportDestination { get; set; }
        public decimal Price { get; set; }

        public static DTOFlightConnection Of(FlightConnection flightConnection)
            => new DTOFlightConnection
            {
                Id = flightConnection.Id,
                IdAirportOrigin = flightConnection.AirportOriginId,
                AcronymAirportOrigin = flightConnection.AirportOrigin.Acronym,
                IdAirportDestination = flightConnection.AirportDestinationId,
                AcronymAirportDestination = flightConnection.AirportDestination.Acronym,
                Price = flightConnection.Price
            };
    }
}
