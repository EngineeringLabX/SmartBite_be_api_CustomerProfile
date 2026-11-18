using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Domain.Interfaces
{
    public interface IAllergenRestrictionRepository
    {
        Task<List<AllergenRestriction>?> GetAllAllergenRestrictionsAsync(int customerId, CancellationToken cancellation);
        Task<AllergenRestriction?> GetAllergenRestrictionAsync(int customerId, int allergenRestrictionId, CancellationToken cancellation);
        Task<AllergenRestriction?> CreateAllergenRestrictionAsync(AllergenRestriction allergenRestriction, CancellationToken cancellation);
        Task<bool> UpdateAllergenRestrictionAsync(AllergenRestriction allergenRestriction, CancellationToken cancellation);
        Task<bool> DeleteAllergenRestrictionAsync(int customerId, int allergenRestrictionId, CancellationToken cancellation);
    }
}
