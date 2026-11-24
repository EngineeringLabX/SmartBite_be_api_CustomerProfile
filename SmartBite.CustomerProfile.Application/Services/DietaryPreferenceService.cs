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
    public class DietaryPreferenceService(IDietaryPreferenceRepository repository, IMapper mapper, ILogger<DietaryPreferenceService> logger) : IDietaryPreferenceService
    {
        private readonly IDietaryPreferenceRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<DietaryPreferenceService> _logger = logger;

        public async Task<SBResponse<List<DietaryPreferenceDto>?>> GetAllDietaryPreferences(int customerId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Dietary preference information ", nameof(DietaryPreferenceService), nameof(GetAllDietaryPreferences));
                var result = await _repository.GetAllDietaryPreferencesAsync(customerId, cancellation);
                return SBResponse<List<DietaryPreferenceDto>?>.Success(result?.Count > 0 ? _mapper.Map<List<DietaryPreferenceDto>>(result) : [], result?.Count > 0 ? "Dietary preference information fetched successfully." : "No Records Found");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Dietary preference information ", nameof(DietaryPreferenceService), nameof(GetAllDietaryPreferences));
                return SBResponse<List<DietaryPreferenceDto>?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Dietary preference information ", nameof(DietaryPreferenceService), nameof(GetAllDietaryPreferences));
                return SBResponse<List<DietaryPreferenceDto>?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<DietaryPreferenceDto?>> GetDietaryPreference(int customerId, int dietaryPreferenceId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Dietary preference information ", nameof(DietaryPreferenceService), nameof(GetDietaryPreference));
                var result = await _repository.GetDietaryPreferenceAsync(customerId, dietaryPreferenceId, cancellation);
                return result == null ? SBResponse<DietaryPreferenceDto?>.Success(null, "No Records Found") : SBResponse<DietaryPreferenceDto?>.Success(_mapper.Map<DietaryPreferenceDto>(result), "Dietary preference information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Dietary preference information ", nameof(DietaryPreferenceService), nameof(GetDietaryPreference));
                return SBResponse<DietaryPreferenceDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Dietary preference information ", nameof(DietaryPreferenceService), nameof(GetDietaryPreference));
                return SBResponse<DietaryPreferenceDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<DietaryPreferenceDto?>> CreateDietaryPreference(DietaryPreferenceDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Creating Dietary preference information ", nameof(DietaryPreferenceService), nameof(CreateDietaryPreference));
                var result = await _repository.CreateDietaryPreferenceAsync(_mapper.Map<Domain.Entities.DietaryPreference>(dto), cancellation);
                return result == null ? SBResponse<DietaryPreferenceDto?>.Failure("Failed to create dietary preference information.", HttpStatusCode.InternalServerError) : SBResponse<DietaryPreferenceDto?>.Success(_mapper.Map<DietaryPreferenceDto>(result), "Dietary preference information created successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Dietary preference information ", nameof(DietaryPreferenceService), nameof(CreateDietaryPreference));
                return SBResponse<DietaryPreferenceDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Dietary preference information ", nameof(DietaryPreferenceService), nameof(CreateDietaryPreference));
                return SBResponse<DietaryPreferenceDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> UpdateDietaryPreference(DietaryPreferenceDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Updating Dietary preference information ", nameof(DietaryPreferenceService), nameof(UpdateDietaryPreference));
                var result = await _repository.UpdateDietaryPreferenceAsync(_mapper.Map<Domain.Entities.DietaryPreference>(dto), cancellation);
                return SBResponse<bool>.Success(result, "Dietary preference information updated successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Dietary preference information ", nameof(DietaryPreferenceService), nameof(UpdateDietaryPreference));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Dietary preference information ", nameof(DietaryPreferenceService), nameof(UpdateDietaryPreference));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> DeleteDietaryPreference(int customerId, int dietaryPreferenceId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Deleting Dietary preference information ", nameof(DietaryPreferenceService), nameof(DeleteDietaryPreference));
                var result = await _repository.DeleteDietaryPreferenceAsync(customerId, dietaryPreferenceId, cancellation);
                return SBResponse<bool>.Success(result, "Dietary preference information deleted successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Dietary preference information ", nameof(DietaryPreferenceService), nameof(DeleteDietaryPreference));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Dietary preference information ", nameof(DietaryPreferenceService), nameof(DeleteDietaryPreference));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
