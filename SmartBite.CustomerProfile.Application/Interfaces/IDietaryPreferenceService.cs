using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;

namespace SmartBite.CustomerProfile.Application.Interfaces
{
    public interface IDietaryPreferenceService
    {
        Task<SBResponse<List<DietaryPreferenceDto>?>> GetAllDietaryPreferences(int customerId, CancellationToken cancellation);
        Task<SBResponse<DietaryPreferenceDto?>> GetDietaryPreference(int customerId, int dietaryPreferenceId, CancellationToken cancellation);
        Task<SBResponse<DietaryPreferenceDto?>> CreateDietaryPreference(DietaryPreferenceDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> UpdateDietaryPreference(DietaryPreferenceDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> DeleteDietaryPreference(int customerId, int dietaryPreferenceId, CancellationToken cancellation);
    }
}
