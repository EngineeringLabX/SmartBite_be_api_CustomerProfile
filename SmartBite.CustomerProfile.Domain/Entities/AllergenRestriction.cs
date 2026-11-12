using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartBite.CustomerProfile.Domain.Enums;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    /// <summary>One allergen the customer must avoid, with its severity tier. Enforcement follows from the severity.</summary>
    [Table("AllergenRestrictions", Schema = "CustomerProfile")]
    public class AllergenRestriction : BaseEntity
    {
        [Key]
        public int AllergenRestrictionId { get; set; }

        public int CustomerId { get; set; }
        public required string AllergenName { get; set; }
        public AllergenSeverity Severity { get; set; }
    }
}
