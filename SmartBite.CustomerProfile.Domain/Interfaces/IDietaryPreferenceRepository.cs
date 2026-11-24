using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Domain.Interfaces
{
    public interface IDietaryPreferenceRepository
    {
        Task<List<DietaryPreference>?> GetAllDietaryPreferencesAsync(int customerId, CancellationToken cancellation);
        Task<DietaryPreference?> GetDietaryPreferenceAsync(int customerId, int dietaryPreferenceId, CancellationToken cancellation);
        Task<DietaryPreference?> CreateDietaryPreferenceAsync(DietaryPreference dietaryPreference, CancellationToken cancellation);
        Task<bool> UpdateDietaryPreferenceAsync(DietaryPreference dietaryPreference, CancellationToken cancellation);
        Task<bool> DeleteDietaryPreferenceAsync(int customerId, int dietaryPreferenceId, CancellationToken cancellation);
    }
}
