using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Domain.Interfaces
{
    public interface IAllergyRepository
    {
        Task<List<Allergy>?> GetAllAllergyAsync(CancellationToken cancellation);
        Task<Allergy?> GetAllergyAsync(int allergyId, CancellationToken cancellation);
        Task<Allergy?> CreateAllergyAsync(Allergy allergy, CancellationToken cancellation);
        Task<bool> UpdateAllergyAsync(Allergy allergy, CancellationToken cancellation);
        Task<bool> DeleteAllergyAsync(int allergyId, CancellationToken cancellation);
    }
}
