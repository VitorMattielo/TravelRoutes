namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOGetFlightConnections
    {
        public List<DTOFlightConnection> FlightConnections { get; set; }

        public DTOGetFlightConnections(List<DTOFlightConnection> flightConnections)
        {
            FlightConnections = flightConnections;
        }
    }
}
