using System.ComponentModel.DataAnnotations;

namespace TravelRoutesManagement.Domain.DTOs
{
    public class DTOCreateAirport
    {
        [Required(ErrorMessage = "O nome do aeroporto é obrigatório!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 100 caracteres!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A sigla do aeroporto é obrigatória!")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "A sigla deve ter exatamente 3 caracteres!")]
        public string Acronym { get; set; }
    }
}
