using System.ComponentModel.DataAnnotations;
using TravelRoutesManagement.Domain.Entities.Abstractions;

namespace TravelRoutesManagement.Domain.Entities
{
    public class FlightConnection : EntityBase
    {
        [Required(ErrorMessage = "O aeroporto de origem é obrigatório!")]
        public int AirportOriginId { get; private set; }
        public virtual Airport AirportOrigin { get; private set; }

        [Required(ErrorMessage = "O aeroporto de destino é obrigatório!")]
        public int AirportDestinationId { get; private set; }
        public virtual Airport AirportDestination { get; private set; }

        [Required(ErrorMessage = "O preço é obrigatório!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser no mínimo 0,01!")]
        public decimal Price { get; private set; }

        public FlightConnection(int airportOriginId, int airportDestinationId, decimal price)
        {
            AirportOriginId = airportOriginId;
            AirportDestinationId = airportDestinationId;
            Price = price;
        }

        public void Update(int airportOriginId, int airportDestinationId, decimal price)
        {
            AirportOriginId = airportOriginId;
            AirportDestinationId = airportDestinationId;
            Price = price;
        }
    }
}
