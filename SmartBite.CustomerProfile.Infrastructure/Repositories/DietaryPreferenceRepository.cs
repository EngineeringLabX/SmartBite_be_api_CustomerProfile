using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Application.Common.Exceptions;
using SmartBite.CustomerProfile.Domain.Entities;
using SmartBite.CustomerProfile.Domain.Interfaces;
using SmartBite.CustomerProfile.Infrastructure.Persistance;
using System.Net;

namespace SmartBite.CustomerProfile.Infrastructure.Repositories
{
    public class DietaryPreferenceRepository(CustomerProfileDbContext context, ILogger<DietaryPreferenceRepository> logger) : IDietaryPreferenceRepository
    {
        private readonly CustomerProfileDbContext _context = context;
        private readonly ILogger<DietaryPreferenceRepository> _logger = logger;

        public async Task<List<DietaryPreference>?> GetAllDietaryPreferencesAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.DietaryPreferences.AsNoTracking().ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching all dietary preference information.", nameof(DietaryPreferenceRepository), nameof(GetAllDietaryPreferencesAsync));
                return null;
            }
        }

        public async Task<DietaryPreference?> GetDietaryPreferenceAsync(int dietaryPreferenceId, CancellationToken cancellation)
        {
            try
            {
                return await _context.DietaryPreferences.AsNoTracking().FirstOrDefaultAsync(d => d.DietaryPreferenceId == dietaryPreferenceId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching dietary preference information for DietaryPreferenceId: {dietaryPreferenceId}.", nameof(DietaryPreferenceRepository), nameof(GetDietaryPreferenceAsync), dietaryPreferenceId);
                return null;
            }
        }

        public async Task<DietaryPreference?> CreateDietaryPreferenceAsync(DietaryPreference dietaryPreference, CancellationToken cancellation)
        {
            try
            {
                await _context.DietaryPreferences.AddAsync(dietaryPreference, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return dietaryPreference;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while creating dietary preference information.", nameof(DietaryPreferenceRepository), nameof(CreateDietaryPreferenceAsync));
                return null;
            }
        }

        public async Task<bool> UpdateDietaryPreferenceAsync(DietaryPreference dietaryPreference, CancellationToken cancellation)
        {
            try
            {
                if (!await _context.DietaryPreferences.AnyAsync(d => d.DietaryPreferenceId == dietaryPreference.DietaryPreferenceId, cancellation))
                {
                    _logger.LogWarning("{className} - {methodName} Dietary preference with DietaryPreferenceId: {dietaryPreferenceId} does not exist. Update operation aborted.", nameof(DietaryPreferenceRepository), nameof(UpdateDietaryPreferenceAsync), dietaryPreference.DietaryPreferenceId);
                    throw new BusinessException($"Dietary preference with DietaryPreferenceId: {dietaryPreference.DietaryPreferenceId} does not exist. Update operation aborted.", HttpStatusCode.NotFound);
                }

                _context.DietaryPreferences.Update(dietaryPreference);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while updating dietary preference information.", nameof(DietaryPreferenceRepository), nameof(UpdateDietaryPreferenceAsync));
                return false;
            }
        }

        public async Task<bool> DeleteDietaryPreferenceAsync(int dietaryPreferenceId, CancellationToken cancellation)
        {
            try
            {
                var dietaryPreference = await _context.DietaryPreferences.FirstOrDefaultAsync(d => d.DietaryPreferenceId == dietaryPreferenceId, cancellation);
                if (dietaryPreference == null)
                {
                    _logger.LogWarning("{className} - {methodName} Dietary preference with DietaryPreferenceId: {dietaryPreferenceId} does not exist. Delete operation aborted.", nameof(DietaryPreferenceRepository), nameof(DeleteDietaryPreferenceAsync), dietaryPreferenceId);
                    throw new BusinessException($"Dietary preference with DietaryPreferenceId: {dietaryPreferenceId} does not exist. Delete operation aborted.", HttpStatusCode.NotFound);
                }

                dietaryPreference.IsActive = false;
                _context.DietaryPreferences.Update(dietaryPreference);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while deleting dietary preference information.", nameof(DietaryPreferenceRepository), nameof(DeleteDietaryPreferenceAsync));
                return false;
            }
        }
    }
}
