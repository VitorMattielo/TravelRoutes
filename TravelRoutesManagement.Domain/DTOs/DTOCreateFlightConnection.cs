using System.ComponentModel.DataAnnotations;
using TravelRoutesManagement.Domain.DTOs.Validators;

namespace TravelRoutesManagement.Domain.DTOs
{
    [DTOFlightConnectionValidator]
    public class DTOCreateFlightConnection
    {
        [Required(ErrorMessage = "O aeroporto de origem é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "O Id do aeroporto de origem deve ser maior que zero!")]
        public int IdAirportOrigin { get; set; }

        [Required(ErrorMessage = "O aeroporto de destino é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "O Id do aeroporto de destino deve ser maior que zero!")]
        public int IdAirportDestination { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser no mínimo 0,01!")]
        public decimal Price { get; set; }
    }
}
