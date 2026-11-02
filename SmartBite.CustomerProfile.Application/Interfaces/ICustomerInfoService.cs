using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;

namespace SmartBite.CustomerProfile.Application.Interfaces
{
    public interface ICustomerInfoService
    {
        Task<SBResponse<List<CustomerInfoDto>?>> GetAllCustomerInfo(CancellationToken cancellation);
        Task<SBResponse<CustomerInfoDto?>> GetCustomerInfo(int customerId, CancellationToken cancellation);
        Task<SBResponse<CustomerInfoDto?>> CreateCustomerInfo(CustomerInfoDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> UpdateCustomerInfo(CustomerInfoDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> DeleteCustomerInfo(int customerId, CancellationToken cancellation);

    }
}
