using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Application.Common.Exceptions;
using SmartBite.CustomerProfile.Domain.Entities;
using SmartBite.CustomerProfile.Domain.Interfaces;
using SmartBite.CustomerProfile.Infrastructure.Persistance;
using System.Net;

namespace SmartBite.CustomerProfile.Infrastructure.Repositories
{
    public class AllergenRestrictionRepository(CustomerProfileDbContext context, ILogger<AllergenRestrictionRepository> logger) : IAllergenRestrictionRepository
    {
        private readonly CustomerProfileDbContext _context = context;
        private readonly ILogger<AllergenRestrictionRepository> _logger = logger;

        public async Task<List<AllergenRestriction>?> GetAllAllergenRestrictionsAsync(int customerId, CancellationToken cancellation)
        {
            try
            {
                return await _context.AllergenRestrictions
                    .AsNoTracking()
                    .Where(a => a.CustomerId == customerId)
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching allergen restrictions for CustomerId: {customerId}.", nameof(AllergenRestrictionRepository), nameof(GetAllAllergenRestrictionsAsync), customerId);
                return null;
            }
        }

        public async Task<AllergenRestriction?> GetAllergenRestrictionAsync(int customerId, int allergenRestrictionId, CancellationToken cancellation)
        {
            try
            {
                return await _context.AllergenRestrictions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.CustomerId == customerId && a.AllergenRestrictionId == allergenRestrictionId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching allergen restriction for CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId}.", nameof(AllergenRestrictionRepository), nameof(GetAllergenRestrictionAsync), customerId, allergenRestrictionId);
                return null;
            }
        }

        public async Task<AllergenRestriction?> CreateAllergenRestrictionAsync(AllergenRestriction allergenRestriction, CancellationToken cancellation)
        {
            try
            {
                await _context.AllergenRestrictions.AddAsync(allergenRestriction, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return allergenRestriction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while creating an allergen restriction.", nameof(AllergenRestrictionRepository), nameof(CreateAllergenRestrictionAsync));
                return null;
            }
        }

        public async Task<bool> UpdateAllergenRestrictionAsync(AllergenRestriction allergenRestriction, CancellationToken cancellation)
        {
            try
            {
                if (!await _context.AllergenRestrictions.AnyAsync(a => a.CustomerId == allergenRestriction.CustomerId && a.AllergenRestrictionId == allergenRestriction.AllergenRestrictionId, cancellation))
                {
                    _logger.LogWarning("{className} - {methodName} Allergen restriction with CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId} does not exist. Update operation aborted.", nameof(AllergenRestrictionRepository), nameof(UpdateAllergenRestrictionAsync), allergenRestriction.CustomerId, allergenRestriction.AllergenRestrictionId);
                    throw new BusinessException($"Allergen restriction with CustomerId: {allergenRestriction.CustomerId}, AllergenRestrictionId: {allergenRestriction.AllergenRestrictionId} does not exist. Update operation aborted.", HttpStatusCode.NotFound);
                }

                _context.AllergenRestrictions.Update(allergenRestriction);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while updating an allergen restriction.", nameof(AllergenRestrictionRepository), nameof(UpdateAllergenRestrictionAsync));
                return false;
            }
        }

        public async Task<bool> DeleteAllergenRestrictionAsync(int customerId, int allergenRestrictionId, CancellationToken cancellation)
        {
            try
            {
                var allergenRestriction = await _context.AllergenRestrictions
                    .FirstOrDefaultAsync(a => a.CustomerId == customerId && a.AllergenRestrictionId == allergenRestrictionId, cancellation);
                if (allergenRestriction == null)
                {
                    _logger.LogWarning("{className} - {methodName} Allergen restriction with CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId} does not exist. Delete operation aborted.", nameof(AllergenRestrictionRepository), nameof(DeleteAllergenRestrictionAsync), customerId, allergenRestrictionId);
                    throw new BusinessException($"Allergen restriction with CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId} does not exist. Delete operation aborted.", HttpStatusCode.NotFound);
                }

                allergenRestriction.IsActive = false;
                _context.AllergenRestrictions.Update(allergenRestriction);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while deleting an allergen restriction.", nameof(AllergenRestrictionRepository), nameof(DeleteAllergenRestrictionAsync));
                return false;
            }
        }
    }
}
