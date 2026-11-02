using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;
using SmartBite.CustomerProfile.Domain.Interfaces;
using System.Net;

namespace SmartBite.CustomerProfile.Application.Services
{
    public class CustomerInfoService(ICustomerInfoRepository repository,IMapper mapper, ILogger<CustomerInfoService> logger) : ICustomerInfoService
    {
        private readonly ICustomerInfoRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CustomerInfoService> _logger = logger;

        public async Task<SBResponse<List<CustomerInfoDto>?>> GetAllCustomerInfo(CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Customer information ", nameof(CustomerInfoService), nameof(GetAllCustomerInfo));
                var result = await _repository.GetAllCustomerInfoAsync(cancellation);
                if(result.Count<=0)
                {
                    return SBResponse<List<CustomerInfoDto>?>.Success(result, "No Records Found");
                }
                return SBResponse<List<CustomerInfoDto>?>.Success(result, "No Records Found");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Customer information ", nameof(CustomerInfoService), nameof(GetAllCustomerInfo));
                return  SBResponse<List<CustomerInfoDto>?>.Failure(HttpStatusCode.InternalServerError, ex.Message);
            }

        }   

    }
}
