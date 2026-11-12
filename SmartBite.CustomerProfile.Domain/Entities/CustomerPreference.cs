using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartBite.CustomerProfile.Domain.Enums;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    /// <summary>
    /// One row per customer: single focus goal, cooking/delivery style, and the Guardian AI toggles.
    /// Defaults match the onboarding screens. Hard block on severe allergens is always on and is not stored.
    /// </summary>
    [Table("CustomerPreferences", Schema = "CustomerProfile")]
    public class CustomerPreference : BaseEntity
    {
        [Key]
        public int CustomerPreferenceId { get; set; }

        public int CustomerId { get; set; }
        public WellnessGoal PrimaryWellnessGoal { get; set; }
        public CookingStyle CookingStyle { get; set; }
        public DeliveryFormat DeliveryFormat { get; set; }

        public bool SmartPrepAssistantEnabled { get; set; } = true;
        public bool KitchenCrossContaminationGuardEnabled { get; set; } = true;
        public bool MicroNudgeRemindersEnabled { get; set; } = true;
        public bool SoftConflictInterceptionEnabled { get; set; } = false;
        public bool AdaptiveProfileLearningEnabled { get; set; } = false;
    }
}
