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
                var result = await _repository.GetAllAllergyAsync(cancellation);
                return SBResponse<List<AllergyDto>?>.Success(result?.Count > 0 ? _mapper.Map<List<AllergyDto>>(result) : [], result?.Count > 0 ? "Allergy information fetched successfully." : "No Records Found");
            }
            catch (BusinessException ex) { return SBResponse<List<AllergyDto>?>.Failure(ex.Message, ex.StatusCode); }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while fetching allergy information."); return SBResponse<List<AllergyDto>?>.Failure(ex.Message, HttpStatusCode.InternalServerError); }
        }

        public async Task<SBResponse<AllergyDto?>> GetAllergy(int allergyId, CancellationToken cancellation)
        {
            try
            {
                var result = await _repository.GetAllergyAsync(allergyId, cancellation);
                return result == null ? SBResponse<AllergyDto?>.Success(null, "No Records Found") : SBResponse<AllergyDto?>.Success(_mapper.Map<AllergyDto>(result), "Allergy information fetched successfully.");
            }
            catch (BusinessException ex) { return SBResponse<AllergyDto?>.Failure(ex.Message, ex.StatusCode); }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while fetching allergy information."); return SBResponse<AllergyDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError); }
        }

        public async Task<SBResponse<AllergyDto?>> CreateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            try
            {
                var result = await _repository.CreateAllergyAsync(_mapper.Map<Domain.Entities.Allergy>(dto), cancellation);
                return result == null ? SBResponse<AllergyDto?>.Failure("Failed to create allergy information.", HttpStatusCode.InternalServerError) : SBResponse<AllergyDto?>.Success(_mapper.Map<AllergyDto>(result), "Allergy information created successfully.");
            }
            catch (BusinessException ex) { return SBResponse<AllergyDto?>.Failure(ex.Message, ex.StatusCode); }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while creating allergy information."); return SBResponse<AllergyDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError); }
        }

        public async Task<SBResponse<bool>> UpdateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            try { return SBResponse<bool>.Success(await _repository.UpdateAllergyAsync(_mapper.Map<Domain.Entities.Allergy>(dto), cancellation), "Allergy information updated successfully."); }
            catch (BusinessException ex) { return SBResponse<bool>.Failure(ex.Message, ex.StatusCode); }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while updating allergy information."); return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError); }
        }

        public async Task<SBResponse<bool>> DeleteAllergy(int allergyId, CancellationToken cancellation)
        {
            try { return SBResponse<bool>.Success(await _repository.DeleteAllergyAsync(allergyId, cancellation), "Allergy information deleted successfully."); }
            catch (BusinessException ex) { return SBResponse<bool>.Failure(ex.Message, ex.StatusCode); }
            catch (Exception ex) { _logger.LogError(ex, "An error occurred while deleting allergy information."); return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError); }
        }
    }
}
