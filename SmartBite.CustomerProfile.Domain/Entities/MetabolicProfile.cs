using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartBite.CustomerProfile.Domain.Enums;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    /// <summary>Body inputs used to calculate the resting metabolic rate and calorie targets (Step 1).</summary>
    [Table("MetabolicProfiles", Schema = "CustomerProfile")]
    public class MetabolicProfile : BaseEntity
    {
        [Key]
        public int MetabolicProfileId { get; set; }

        public int CustomerId { get; set; }
        public BiologicalSex BiologicalSex { get; set; }
        public int AgeYears { get; set; }
        public decimal HeightCm { get; set; }
        public decimal WeightKg { get; set; }
    }
}
