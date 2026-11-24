using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;

namespace SmartBite.CustomerProfile.Application.Interfaces
{
    public interface IDietPatternService
    {
        Task<SBResponse<List<DietPatternDto>?>> GetAllDietPatterns(CancellationToken cancellation);
        Task<SBResponse<DietPatternDto?>> GetDietPattern(int dietPatternId, CancellationToken cancellation);
        Task<SBResponse<DietPatternDto?>> CreateDietPattern(DietPatternDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> UpdateDietPattern(DietPatternDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> DeleteDietPattern(int dietPatternId, CancellationToken cancellation);
    }
}
