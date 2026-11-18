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
    public class AllergyService(IAllergyRepository repository, IMapper mapper, ILogger<AllergyService> logger) : IAllergyService
    {
        private readonly IAllergyRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AllergyService> _logger = logger;

        public async Task<SBResponse<List<AllergyDto>?>> GetAllAllergy(CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Allergy information ", nameof(AllergyService), nameof(GetAllAllergy));
                var result = await _repository.GetAllAllergyAsync(cancellation);
                return SBResponse<List<AllergyDto>?>.Success(result?.Count > 0 ? _mapper.Map<List<AllergyDto>>(result) : [], result?.Count > 0 ? "Allergy information fetched successfully." : "No Records Found");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergy information ", nameof(AllergyService), nameof(GetAllAllergy));
                return SBResponse<List<AllergyDto>?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergy information ", nameof(AllergyService), nameof(GetAllAllergy));
                return SBResponse<List<AllergyDto>?>.Failure(ex.Message, HttpStatusCode.InternalServerError); }
        }

        public async Task<SBResponse<AllergyDto?>> GetAllergy(int allergyId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Allergy information ", nameof(AllergyService), nameof(GetAllAllergy));
                var result = await _repository.GetAllergyAsync(allergyId, cancellation);
                return result == null ? SBResponse<AllergyDto?>.Success(null, "No Records Found") : SBResponse<AllergyDto?>.Success(_mapper.Map<AllergyDto>(result), "Allergy information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergy information ", nameof(AllergyService), nameof(GetAllAllergy));
                return SBResponse<AllergyDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Allergy information ", nameof(AllergyService), nameof(GetAllAllergy));
                return SBResponse<AllergyDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<AllergyDto?>> CreateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Creating Allergy information ", nameof(AllergyService), nameof(CreateAllergy));
                var result = await _repository.CreateAllergyAsync(_mapper.Map<Domain.Entities.Allergy>(dto), cancellation);
                return result == null ? SBResponse<AllergyDto?>.Failure("Failed to create allergy information.", HttpStatusCode.InternalServerError) : SBResponse<AllergyDto?>.Success(_mapper.Map<AllergyDto>(result), "Allergy information created successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Allergy information ", nameof(AllergyService), nameof(CreateAllergy));
                return SBResponse<AllergyDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Allergy information ", nameof(AllergyService), nameof(CreateAllergy));
                return SBResponse<AllergyDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> UpdateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Updating Allergy information ", nameof(AllergyService), nameof(UpdateAllergy));
                return SBResponse<bool>.Success(await _repository.UpdateAllergyAsync(_mapper.Map<Domain.Entities.Allergy>(dto), cancellation), "Allergy information updated successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Allergy information ", nameof(AllergyService), nameof(UpdateAllergy));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Allergy information ", nameof(AllergyService), nameof(UpdateAllergy));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> DeleteAllergy(int allergyId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Deleting Allergy information ", nameof(AllergyService), nameof(DeleteAllergy));
                return SBResponse<bool>.Success(await _repository.DeleteAllergyAsync(allergyId, cancellation), "Allergy information deleted successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Allergy information ", nameof(AllergyService), nameof(DeleteAllergy));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Allergy information ", nameof(AllergyService), nameof(DeleteAllergy));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
