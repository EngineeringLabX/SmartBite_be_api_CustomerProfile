using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Domain.Interfaces
{
    public interface IDietaryPreferenceRepository
    {
        Task<List<DietaryPreference>?> GetAllDietaryPreferencesAsync(CancellationToken cancellation);
        Task<DietaryPreference?> GetDietaryPreferenceAsync(int dietaryPreferenceId, CancellationToken cancellation);
        Task<DietaryPreference?> CreateDietaryPreferenceAsync(DietaryPreference dietaryPreference, CancellationToken cancellation);
        Task<bool> UpdateDietaryPreferenceAsync(DietaryPreference dietaryPreference, CancellationToken cancellation);
        Task<bool> DeleteDietaryPreferenceAsync(int dietaryPreferenceId, CancellationToken cancellation);
    }
}
