using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartBite.CustomerProfile.Domain.Enums;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    /// <summary>One eating window per meal type for the customer's daily routine (Step 4).</summary>
    [Table("MealSchedules", Schema = "CustomerProfile")]
    public class MealSchedule : BaseEntity
    {
        [Key]
        public int MealScheduleId { get; set; }

        public int CustomerId { get; set; }
        public MealType MealType { get; set; }
        public TimeOnly WindowStart { get; set; }
        public TimeOnly WindowEnd { get; set; }
        public int TargetCalories { get; set; }
    }
}
