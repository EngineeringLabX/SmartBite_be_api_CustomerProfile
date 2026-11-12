using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Application.Common.Exceptions;
using SmartBite.CustomerProfile.Domain.Entities;
using SmartBite.CustomerProfile.Domain.Interfaces;
using SmartBite.CustomerProfile.Infrastructure.Persistance;
using System.Net;

namespace SmartBite.CustomerProfile.Infrastructure.Repositories
{
    public class AllergyRepository(CustomerProfileDbContext context, ILogger<AllergyRepository> logger) : IAllergyRepository
    {
        private readonly CustomerProfileDbContext _context = context;
        private readonly ILogger<AllergyRepository> _logger = logger;

        public async Task<List<Allergy>?> GetAllAllergyAsync(CancellationToken cancellation)
        {
            try { return await _context.Allergies.AsNoTracking().ToListAsync(cancellation); }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while fetching all allergies."); return null; }
        }

        public async Task<Allergy?> GetAllergyAsync(int allergyId, CancellationToken cancellation)
        {
            try { return await _context.Allergies.AsNoTracking().FirstOrDefaultAsync(a => a.AllergyId == allergyId, cancellation); }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while fetching allergy for AllergyId: {allergyId}.", allergyId); return null; }
        }

        public async Task<Allergy?> CreateAllergyAsync(Allergy allergy, CancellationToken cancellation)
        {
            try { await _context.Allergies.AddAsync(allergy, cancellation); await _context.SaveChangesAsync(cancellation); return allergy; }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while creating an allergy."); return null; }
        }

        public async Task<bool> UpdateAllergyAsync(Allergy allergy, CancellationToken cancellation)
        {
            try
            {
                if (!await _context.Allergies.AnyAsync(a => a.AllergyId == allergy.AllergyId, cancellation))
                    throw new BusinessException($"Allergy with AllergyId: {allergy.AllergyId} does not exist. Update operation aborted.", HttpStatusCode.NotFound);
                _context.Allergies.Update(allergy);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while updating allergy information."); return false; }
        }

        public async Task<bool> DeleteAllergyAsync(int allergyId, CancellationToken cancellation)
        {
            try
            {
                var allergy = await _context.Allergies.FirstOrDefaultAsync(a => a.AllergyId == allergyId, cancellation);
                if (allergy == null) throw new BusinessException($"Allergy with AllergyId: {allergyId} does not exist. Delete operation aborted.", HttpStatusCode.NotFound);
                allergy.IsActive = false;
                _context.Allergies.Update(allergy);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while deleting allergy information."); return false; }
        }
    }
}
