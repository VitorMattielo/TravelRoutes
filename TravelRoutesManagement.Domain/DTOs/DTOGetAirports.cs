namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOGetAirports
    {
        public List<DTOAirport> Airports { get; set; }

        public DTOGetAirports(List<DTOAirport> airports)
        {
            Airports = airports;
        }
    }
}
