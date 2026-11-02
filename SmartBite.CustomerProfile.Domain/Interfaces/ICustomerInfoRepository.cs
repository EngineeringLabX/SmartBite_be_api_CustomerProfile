using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Domain.Interfaces
{
    public interface ICustomerInfoRepository
    {
        Task<List<CustomerInfo>?> GetAllCustomerInfoAsync(CancellationToken cancellation);
        Task<CustomerInfo?> GetCustomerInfoAsync(int customerId, CancellationToken cancellation);
        Task<CustomerInfo?> CreateCustomerInfoAsync(CustomerInfo customerInfo, CancellationToken cancellation);
        Task<bool> UpdateCustomerInfoAsync(CustomerInfo customerInfo, CancellationToken cancellation);
        Task<bool> DeleteCustomerInfoAsync(int customerId, CancellationToken cancellation);

    }
}
