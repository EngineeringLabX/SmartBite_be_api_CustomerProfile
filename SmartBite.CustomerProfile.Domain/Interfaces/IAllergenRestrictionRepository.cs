using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Domain.Interfaces
{
    public interface IAllergenRestrictionRepository
    {
        Task<List<AllergenRestriction>?> GetAllAllergenRestrictionsAsync(CancellationToken cancellation);
        Task<AllergenRestriction?> GetAllergenRestrictionAsync(int allergenRestrictionId, CancellationToken cancellation);
        Task<AllergenRestriction?> CreateAllergenRestrictionAsync(AllergenRestriction allergenRestriction, CancellationToken cancellation);
        Task<bool> UpdateAllergenRestrictionAsync(AllergenRestriction allergenRestriction, CancellationToken cancellation);
        Task<bool> DeleteAllergenRestrictionAsync(int allergenRestrictionId, CancellationToken cancellation);
    }
}
