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
    public class DietPatternService(IDietPatternRepository repository, IMapper mapper, ILogger<DietPatternService> logger) : IDietPatternService
    {
        private readonly IDietPatternRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<DietPatternService> _logger = logger;

        public async Task<SBResponse<List<DietPatternDto>?>> GetAllDietPatterns(CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Diet pattern information ", nameof(DietPatternService), nameof(GetAllDietPatterns));
                var result = await _repository.GetAllDietPatternsAsync(cancellation);
                return SBResponse<List<DietPatternDto>?>.Success(result?.Count > 0 ? _mapper.Map<List<DietPatternDto>>(result) : [], result?.Count > 0 ? "Diet pattern information fetched successfully." : "No Records Found");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Diet pattern information ", nameof(DietPatternService), nameof(GetAllDietPatterns));
                return SBResponse<List<DietPatternDto>?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Diet pattern information ", nameof(DietPatternService), nameof(GetAllDietPatterns));
                return SBResponse<List<DietPatternDto>?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<DietPatternDto?>> GetDietPattern(int dietPatternId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Diet pattern information ", nameof(DietPatternService), nameof(GetDietPattern));
                var result = await _repository.GetDietPatternAsync(dietPatternId, cancellation);
                return result == null
                    ? SBResponse<DietPatternDto?>.Success(null, "No Records Found")
                    : SBResponse<DietPatternDto?>.Success(_mapper.Map<DietPatternDto>(result), "Diet pattern information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Diet pattern information ", nameof(DietPatternService), nameof(GetDietPattern));
                return SBResponse<DietPatternDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Fetching Diet pattern information ", nameof(DietPatternService), nameof(GetDietPattern));
                return SBResponse<DietPatternDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<DietPatternDto?>> CreateDietPattern(DietPatternDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Creating Diet pattern information ", nameof(DietPatternService), nameof(CreateDietPattern));
                var result = await _repository.CreateDietPatternAsync(_mapper.Map<Domain.Entities.DietPattern>(dto), cancellation);
                return result == null
                    ? SBResponse<DietPatternDto?>.Failure("Failed to create diet pattern information.", HttpStatusCode.InternalServerError)
                    : SBResponse<DietPatternDto?>.Success(_mapper.Map<DietPatternDto>(result), "Diet pattern information created successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Diet pattern information ", nameof(DietPatternService), nameof(CreateDietPattern));
                return SBResponse<DietPatternDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Creating Diet pattern information ", nameof(DietPatternService), nameof(CreateDietPattern));
                return SBResponse<DietPatternDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> UpdateDietPattern(DietPatternDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Updating Diet pattern information ", nameof(DietPatternService), nameof(UpdateDietPattern));
                var result = await _repository.UpdateDietPatternAsync(_mapper.Map<Domain.Entities.DietPattern>(dto), cancellation);
                return SBResponse<bool>.Success(result, "Diet pattern information updated successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Diet pattern information ", nameof(DietPatternService), nameof(UpdateDietPattern));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Updating Diet pattern information ", nameof(DietPatternService), nameof(UpdateDietPattern));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> DeleteDietPattern(int dietPatternId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Deleting Diet pattern information ", nameof(DietPatternService), nameof(DeleteDietPattern));
                var result = await _repository.DeleteDietPatternAsync(dietPatternId, cancellation);
                return SBResponse<bool>.Success(result, "Diet pattern information deleted successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Diet pattern information ", nameof(DietPatternService), nameof(DeleteDietPattern));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured {className} - {methodName} While Deleting Diet pattern information ", nameof(DietPatternService), nameof(DeleteDietPattern));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
