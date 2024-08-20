using System.ComponentModel.DataAnnotations;
using TravelRoutesManagement.Domain.Entities.Abstractions;

namespace TravelRoutesManagement.Domain.Entities
{
    public class Airport : EntityBase
    {
        [Required(ErrorMessage = "O nome do aeroporto é obrigatório!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 100 caracteres!")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "A sigla do aeroporto é obrigatória!")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "A sigla deve ter exatamente 3 caracteres!")]
        public string Acronym { get; private set; }

        public Airport(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }

        public void Update(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }
    }
}
