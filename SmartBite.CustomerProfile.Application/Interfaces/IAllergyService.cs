using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;

namespace SmartBite.CustomerProfile.Application.Interfaces
{
    public interface IAllergyService
    {
        Task<SBResponse<List<AllergyDto>?>> GetAllAllergy(CancellationToken cancellation);
        Task<SBResponse<AllergyDto?>> GetAllergy(int allergyId, CancellationToken cancellation);
        Task<SBResponse<AllergyDto?>> CreateAllergy(AllergyDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> UpdateAllergy(AllergyDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> DeleteAllergy(int allergyId, CancellationToken cancellation);
    }
}
