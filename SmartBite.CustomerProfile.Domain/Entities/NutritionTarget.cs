using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    /// <summary>
    /// One active row per customer: daily energy budget, macro split, fibre and clinical caps (Step 3).
    /// Nullable caps mean the clinical safeguard is switched off.
    /// </summary>
    [Table("NutritionTargets", Schema = "CustomerProfile")]
    public class NutritionTarget : BaseEntity
    {
        [Key]
        public int NutritionTargetId { get; set; }

        public int CustomerId { get; set; }

        /// <summary>Resting metabolic rate snapshot used for this target (kcal/day).</summary>
        public int RestingMetabolicRate { get; set; }

        /// <summary>Daily calorie budget after goal adjustment (kcal/day).</summary>
        public int DailyCalorieTarget { get; set; }

        public int ProteinGrams { get; set; }
        public int HealthyFatGrams { get; set; }
        public int CarbohydrateGrams { get; set; }
        public int FiberGramsMin { get; set; }

        /// <summary>Daily sodium ceiling in mg. Null = cap off.</summary>
        public int? SodiumLimitMg { get; set; }

        /// <summary>Maximum glycemic load per meal. Null = safeguard off.</summary>
        public int? GlycemicLoadCap { get; set; }
    }
}
