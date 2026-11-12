using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartBite.CustomerProfile.Domain.Enums;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    /// <summary>One selected core diet pattern. A customer can have many (multi-select).</summary>
    [Table("DietaryPreferences", Schema = "CustomerProfile")]
    public class DietaryPreference : BaseEntity
    {
        [Key]
        public int DietaryPreferenceId { get; set; }

        public int CustomerId { get; set; }
        public DietPattern DietPattern { get; set; }
    }
}
