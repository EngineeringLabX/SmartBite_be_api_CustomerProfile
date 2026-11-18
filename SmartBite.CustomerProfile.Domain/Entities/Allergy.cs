using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    [Table("Allergies", Schema = "CustomerProfile")]
    public class Allergy : BaseEntity
    {
        [Key]
        public int AllergyId { get; set; }
        public required string AllergyName { get; set; }
    }
}
