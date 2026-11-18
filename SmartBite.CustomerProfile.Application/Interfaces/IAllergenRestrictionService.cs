using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;

namespace SmartBite.CustomerProfile.Application.Interfaces
{
    public interface IAllergenRestrictionService
    {
        Task<SBResponse<List<AllergenRestrictionDto>?>> GetAllAllergenRestrictions(int customerId, CancellationToken cancellation);
        Task<SBResponse<AllergenRestrictionDto?>> GetAllergenRestriction(int customerId, int allergenRestrictionId, CancellationToken cancellation);
        Task<SBResponse<AllergenRestrictionDto?>> CreateAllergenRestriction(AllergenRestrictionDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> UpdateAllergenRestriction(AllergenRestrictionDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> DeleteAllergenRestriction(int customerId, int allergenRestrictionId, CancellationToken cancellation);
    }
}
