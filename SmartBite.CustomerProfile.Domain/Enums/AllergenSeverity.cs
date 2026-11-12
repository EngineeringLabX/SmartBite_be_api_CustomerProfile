namespace SmartBite.CustomerProfile.Domain.Enums
{
    /// <summary>
    /// Allergen severity tiers from the Allergens step. The enforcement behaviour is derived from the tier:
    /// Anaphylaxis = strict hard block, Severe = dedicated prep only, Mild = soft substitution prompt.
    /// </summary>
    public enum AllergenSeverity
    {
        Mild = 1,          // Level 1
        Severe = 2,        // Level 2
        Anaphylaxis = 3    // Level 3
    }
}
