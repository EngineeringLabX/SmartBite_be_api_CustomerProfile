using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Application.Common.Exceptions;
using SmartBite.CustomerProfile.Domain.Entities;
using SmartBite.CustomerProfile.Domain.Interfaces;
using SmartBite.CustomerProfile.Infrastructure.Persistance;
using System.Net;

namespace SmartBite.CustomerProfile.Infrastructure.Repositories
{
    public class CustomerInfoRepository(CustomerProfileDbContext context, ILogger<CustomerInfoRepository> logger) : ICustomerInfoRepository
    {
        private readonly CustomerProfileDbContext _context = context;
        private readonly ILogger<CustomerInfoRepository> _logger = logger;

        public async Task<List<CustomerInfo>?> GetAllCustomerInfoAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.CustomerInfo.AsNoTracking().ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching all customer information.", nameof(CustomerInfoRepository), nameof(GetAllCustomerInfoAsync));
                return null;
            }
        }

        public async Task<CustomerInfo?> GetCustomerInfoAsync(int customerId, CancellationToken cancellation)
        {
            try
            {
                return await _context.CustomerInfo.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellation);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching customer information for CustomerId: {customerId}.", nameof(CustomerInfoRepository), nameof(GetCustomerInfoAsync), customerId);
                return null;
            }
        }

        public async Task<CustomerInfo?> CreateCustomerInfoAsync(CustomerInfo customerInfo, CancellationToken cancellation)
        {
            try
            {
                await _context.CustomerInfo.AddAsync(customerInfo, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return customerInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while creating customer information.", nameof(CustomerInfoRepository), nameof(CreateCustomerInfoAsync));
                return null;
            }
        }

        public async Task<bool> UpdateCustomerInfoAsync(CustomerInfo customerInfo, CancellationToken cancellation)
        {
            try
            {
                var isExist = await _context.CustomerInfo.AnyAsync(c => c.CustomerId == customerInfo.CustomerId, cancellation);
                if (!isExist)
                {
                    _logger.LogWarning("{className} - {methodName} Customer information with CustomerId: {customerId} does not exist. Update operation aborted.", nameof(CustomerInfoRepository), nameof(UpdateCustomerInfoAsync), customerInfo.CustomerId);
                    throw new BusinessException($"Customer information with CustomerId: {customerInfo.CustomerId} does not exist. Update operation aborted.", HttpStatusCode.NotFound);
                }
                _context.CustomerInfo.Update(customerInfo);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while updating customer information.", nameof(CustomerInfoRepository), nameof(UpdateCustomerInfoAsync));
                return false;
            }
        }

        public async Task<bool> DeleteCustomerInfoAsync(int customerId, CancellationToken cancellation)
        {
            try
            {
                var customerInfo = await _context.CustomerInfo.FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellation);
                if (customerInfo == null)
                {
                    _logger.LogWarning("{className} - {methodName} Customer information with CustomerId: {customerId} does not exist. Delete operation aborted.", nameof(CustomerInfoRepository), nameof(DeleteCustomerInfoAsync), customerId);
                    throw new BusinessException($"Customer information with CustomerId: {customerId} does not exist. Delete operation aborted.", HttpStatusCode.NotFound);
                }

                _context.CustomerInfo.Remove(customerInfo);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while deleting customer information.", nameof(CustomerInfoRepository), nameof(DeleteCustomerInfoAsync));
                return false;
            }
        }
    }
}
