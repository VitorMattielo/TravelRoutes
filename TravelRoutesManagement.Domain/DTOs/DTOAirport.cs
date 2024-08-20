using TravelRoutesManagement.Domain.Entities;

namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOAirport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }

        public static DTOAirport Of(Airport airport) 
            => new DTOAirport { Id = airport.Id, Name = airport.Name, Acronym = airport.Acronym };
    }
}
