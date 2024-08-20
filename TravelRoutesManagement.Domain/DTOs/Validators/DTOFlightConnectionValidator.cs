using System.ComponentModel.DataAnnotations;

namespace TravelRoutesManagement.Domain.DTOs.Validators
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class DTOFlightConnectionValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (DTOCreateFlightConnection)validationContext.ObjectInstance;

            if (model.IdAirportOrigin == model.IdAirportDestination)
                return new ValidationResult("O aeroporto de origem não pode ser o mesmo que o aeroporto de destino.");

            return ValidationResult.Success;
        }
    }
}
