using System.ComponentModel.DataAnnotations;

namespace TravelRoutesManagement.Domain.Entities.Abstractions
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; private set; }
    }
}
