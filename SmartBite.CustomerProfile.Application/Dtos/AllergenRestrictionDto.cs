using SmartBite.CustomerProfile.Domain.Enums;

namespace SmartBite.CustomerProfile.Application.Dtos
{
    public record AllergenRestrictionDto
    {
        public int AllergenRestrictionId { get; set; }
        public int CustomerId { get; set; }
        public int AllergyId { get; set; }
        public AllergenSeverity Severity { get; set; }
    }
}
