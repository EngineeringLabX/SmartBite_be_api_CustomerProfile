using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Application.Common.Exceptions;
using SmartBite.CustomerProfile.Domain.Entities;
using SmartBite.CustomerProfile.Domain.Interfaces;
using SmartBite.CustomerProfile.Infrastructure.Persistance;
using System.Net;

namespace SmartBite.CustomerProfile.Infrastructure.Repositories
{
    public class DietPatternRepository(CustomerProfileDbContext context, ILogger<DietPatternRepository> logger) : IDietPatternRepository
    {
        private readonly CustomerProfileDbContext _context = context;
        private readonly ILogger<DietPatternRepository> _logger = logger;

        public async Task<List<DietPattern>?> GetAllDietPatternsAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.DietPatterns.AsNoTracking().ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching all diet pattern information.", nameof(DietPatternRepository), nameof(GetAllDietPatternsAsync));
                return null;
            }
        }

        public async Task<DietPattern?> GetDietPatternAsync(int dietPatternId, CancellationToken cancellation)
        {
            try
            {
                return await _context.DietPatterns.AsNoTracking().FirstOrDefaultAsync(d => d.DietPatternId == dietPatternId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching diet pattern information for DietPatternId: {dietPatternId}.", nameof(DietPatternRepository), nameof(GetDietPatternAsync), dietPatternId);
                return null;
            }
        }

        public async Task<DietPattern?> CreateDietPatternAsync(DietPattern dietPattern, CancellationToken cancellation)
        {
            try
            {
                await _context.DietPatterns.AddAsync(dietPattern, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return dietPattern;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while creating diet pattern information.", nameof(DietPatternRepository), nameof(CreateDietPatternAsync));
                return null;
            }
        }

        public async Task<bool> UpdateDietPatternAsync(DietPattern dietPattern, CancellationToken cancellation)
        {
            try
            {
                if (!await _context.DietPatterns.AnyAsync(d => d.DietPatternId == dietPattern.DietPatternId, cancellation))
                {
                    _logger.LogWarning("{className} - {methodName} Diet pattern with DietPatternId: {dietPatternId} does not exist. Update operation aborted.", nameof(DietPatternRepository), nameof(UpdateDietPatternAsync), dietPattern.DietPatternId);
                    throw new BusinessException($"Diet pattern with DietPatternId: {dietPattern.DietPatternId} does not exist. Update operation aborted.", HttpStatusCode.NotFound);
                }

                _context.DietPatterns.Update(dietPattern);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while updating diet pattern information.", nameof(DietPatternRepository), nameof(UpdateDietPatternAsync));
                return false;
            }
        }

        public async Task<bool> DeleteDietPatternAsync(int dietPatternId, CancellationToken cancellation)
        {
            try
            {
                var dietPattern = await _context.DietPatterns.FirstOrDefaultAsync(d => d.DietPatternId == dietPatternId, cancellation);
                if (dietPattern == null)
                {
                    _logger.LogWarning("{className} - {methodName} Diet pattern with DietPatternId: {dietPatternId} does not exist. Delete operation aborted.", nameof(DietPatternRepository), nameof(DeleteDietPatternAsync), dietPatternId);
                    throw new BusinessException($"Diet pattern with DietPatternId: {dietPatternId} does not exist. Delete operation aborted.", HttpStatusCode.NotFound);
                }

                dietPattern.IsActive = false;
                _context.DietPatterns.Update(dietPattern);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while deleting diet pattern information.", nameof(DietPatternRepository), nameof(DeleteDietPatternAsync));
                return false;
            }
        }
    }
}
