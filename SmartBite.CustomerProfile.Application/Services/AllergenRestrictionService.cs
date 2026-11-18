using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Application.Common.Exceptions;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;
using SmartBite.CustomerProfile.Domain.Interfaces;
using System.Net;

namespace SmartBite.CustomerProfile.Application.Services
{
    public class AllergenRestrictionService(IAllergenRestrictionRepository repository, IMapper mapper, ILogger<AllergenRestrictionService> logger) : IAllergenRestrictionService
    {
        private readonly IAllergenRestrictionRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AllergenRestrictionService> _logger = logger;

        public async Task<SBResponse<List<AllergenRestrictionDto>?>> GetAllAllergenRestrictions(int customerId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Allergen restriction information ", nameof(AllergenRestrictionService), nameof(GetAllAllergenRestrictions));
                var result = await _repository.GetAllAllergenRestrictionsAsync(customerId, cancellation);
                return SBResponse<List<AllergenRestrictionDto>?>.Success(result?.Count > 0 ? _mapper.Map<List<AllergenRestrictionDto>>(result) : [], result?.Count > 0 ? "Allergen restriction information fetched successfully." : "No Records Found");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergen restriction information ", nameof(AllergenRestrictionService), nameof(GetAllAllergenRestrictions));
                return SBResponse<List<AllergenRestrictionDto>?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergen restriction information ", nameof(AllergenRestrictionService), nameof(GetAllAllergenRestrictions));
                return SBResponse<List<AllergenRestrictionDto>?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<AllergenRestrictionDto?>> GetAllergenRestriction(int customerId, int allergenRestrictionId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Allergen restriction information ", nameof(AllergenRestrictionService), nameof(GetAllergenRestriction));
                var result = await _repository.GetAllergenRestrictionAsync(customerId, allergenRestrictionId, cancellation);
                return result == null ? SBResponse<AllergenRestrictionDto?>.Success(null, "No Records Found") : SBResponse<AllergenRestrictionDto?>.Success(_mapper.Map<AllergenRestrictionDto>(result), "Allergen restriction information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergen restriction information ", nameof(AllergenRestrictionService), nameof(GetAllergenRestriction));
                return SBResponse<AllergenRestrictionDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergen restriction information ", nameof(AllergenRestrictionService), nameof(GetAllergenRestriction));
                return SBResponse<AllergenRestrictionDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<AllergenRestrictionDto?>> CreateAllergenRestriction(AllergenRestrictionDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Creating Allergen restriction information ", nameof(AllergenRestrictionService), nameof(CreateAllergenRestriction));
                var result = await _repository.CreateAllergenRestrictionAsync(_mapper.Map<Domain.Entities.AllergenRestriction>(dto), cancellation);
                return result == null ? SBResponse<AllergenRestrictionDto?>.Failure("Failed to create allergen restriction information.", HttpStatusCode.InternalServerError) : SBResponse<AllergenRestrictionDto?>.Success(_mapper.Map<AllergenRestrictionDto>(result), "Allergen restriction information created successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Allergen restriction information ", nameof(AllergenRestrictionService), nameof(CreateAllergenRestriction));
                return SBResponse<AllergenRestrictionDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Allergen restriction information ", nameof(AllergenRestrictionService), nameof(CreateAllergenRestriction));
                return SBResponse<AllergenRestrictionDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> UpdateAllergenRestriction(AllergenRestrictionDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Updating Allergen restriction information ", nameof(AllergenRestrictionService), nameof(UpdateAllergenRestriction));
                return SBResponse<bool>.Success(await _repository.UpdateAllergenRestrictionAsync(_mapper.Map<Domain.Entities.AllergenRestriction>(dto), cancellation), "Allergen restriction information updated successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Allergen restriction information ", nameof(AllergenRestrictionService), nameof(UpdateAllergenRestriction));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Allergen restriction information ", nameof(AllergenRestrictionService), nameof(UpdateAllergenRestriction));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> DeleteAllergenRestriction(int customerId, int allergenRestrictionId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Deleting Allergen restriction information ", nameof(AllergenRestrictionService), nameof(DeleteAllergenRestriction));
                return SBResponse<bool>.Success(await _repository.DeleteAllergenRestrictionAsync(customerId, allergenRestrictionId, cancellation), "Allergen restriction information deleted successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Allergen restriction information ", nameof(AllergenRestrictionService), nameof(DeleteAllergenRestriction));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Allergen restriction information ", nameof(AllergenRestrictionService), nameof(DeleteAllergenRestriction));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
