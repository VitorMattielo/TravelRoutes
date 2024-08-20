using System.ComponentModel.DataAnnotations;

namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOUpdateFlightConnection : DTOCreateFlightConnection
    {
        [Required(ErrorMessage = "O ID da conexão é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "O Id da conexão deve ser maior que zero!")]
        public int Id { get; set; }
    }
}
