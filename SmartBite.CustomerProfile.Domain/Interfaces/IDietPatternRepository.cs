using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Domain.Interfaces
{
    public interface IDietPatternRepository
    {
        Task<List<DietPattern>?> GetAllDietPatternsAsync(CancellationToken cancellation);
        Task<DietPattern?> GetDietPatternAsync(int dietPatternId, CancellationToken cancellation);
        Task<DietPattern?> CreateDietPatternAsync(DietPattern dietPattern, CancellationToken cancellation);
        Task<bool> UpdateDietPatternAsync(DietPattern dietPattern, CancellationToken cancellation);
        Task<bool> DeleteDietPatternAsync(int dietPatternId, CancellationToken cancellation);
    }
}
