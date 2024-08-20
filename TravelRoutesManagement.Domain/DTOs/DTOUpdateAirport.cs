using System.ComponentModel.DataAnnotations;

namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOUpdateAirport : DTOCreateAirport
    {
        [Required(ErrorMessage = "O ID do aeroporto é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "O Id do aeroporto deve ser maior que zero!")]
        public int Id { get; set; }
    }
}
