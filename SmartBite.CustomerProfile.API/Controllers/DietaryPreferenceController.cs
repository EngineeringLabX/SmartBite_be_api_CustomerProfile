using Microsoft.AspNetCore.Mvc;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;

namespace SmartBite.CustomerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietaryPreferenceController(IDietaryPreferenceService dietaryPreferenceService, ILogger<DietaryPreferenceController> logger) : ControllerBase
    {
        private readonly IDietaryPreferenceService _dietaryPreferenceService = dietaryPreferenceService;
        private readonly ILogger<DietaryPreferenceController> _logger = logger;

        [HttpGet("GetAllDietaryPreferences/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<DietaryPreferenceDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<DietaryPreferenceDto>?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<List<DietaryPreferenceDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<DietaryPreferenceDto>?>>> GetAllDietaryPreferences(int customerId, CancellationToken cancellation)
        {
            if (customerId <= 0)
            {
                _logger.LogWarning("Invalid customerId provided: {customerId}. It must be a positive integer.", customerId);
                return BadRequest(SBResponse<List<DietaryPreferenceDto>?>.Failure("Invalid customerId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _dietaryPreferenceService.GetAllDietaryPreferences(customerId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching all dietary preference information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all dietary preference information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<List<DietaryPreferenceDto>?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("GetDietaryPreference/{customerId}/{dietaryPreferenceId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<DietaryPreferenceDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<DietaryPreferenceDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<DietaryPreferenceDto?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<DietaryPreferenceDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<DietaryPreferenceDto?>>> GetDietaryPreference(int customerId, int dietaryPreferenceId, CancellationToken cancellation)
        {
            if (customerId <= 0 || dietaryPreferenceId <= 0)
            {
                _logger.LogWarning("Invalid customerId or dietaryPreferenceId provided. CustomerId: {customerId}, DietaryPreferenceId: {dietaryPreferenceId}.", customerId, dietaryPreferenceId);
                return BadRequest(SBResponse<DietaryPreferenceDto?>.Failure("Invalid customerId and dietaryPreferenceId. Both must be positive integers.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietaryPreferenceService.GetDietaryPreference(customerId, dietaryPreferenceId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching dietary preference information for CustomerId: {customerId}, DietaryPreferenceId: {dietaryPreferenceId}. Error: {errorMessage}", customerId, dietaryPreferenceId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching dietary preference information for CustomerId: {customerId}, DietaryPreferenceId: {dietaryPreferenceId}.", customerId, dietaryPreferenceId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<DietaryPreferenceDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("CreateDietaryPreference")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<DietaryPreferenceDto?>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SBResponse<DietaryPreferenceDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<DietaryPreferenceDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<DietaryPreferenceDto?>>> CreateDietaryPreference(DietaryPreferenceDto dto, CancellationToken cancellation)
        {
            if (dto == null || dto.CustomerId <= 0 || dto.DietPatternId <= 0 || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid DietaryPreferenceDto for creation.");
                return BadRequest(SBResponse<DietaryPreferenceDto?>.Failure("DietaryPreferenceDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietaryPreferenceService.CreateDietaryPreference(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while creating dietary preference information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating dietary preference information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<DietaryPreferenceDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("UpdateDietaryPreference")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> UpdateDietaryPreference(DietaryPreferenceDto dto, CancellationToken cancellation)
        {
            if (dto == null || dto.DietaryPreferenceId <= 0 || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid DietaryPreferenceDto for update.");
                return BadRequest(SBResponse<bool>.Failure("DietaryPreferenceDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietaryPreferenceService.UpdateDietaryPreference(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while updating dietary preference information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating dietary preference information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("DeleteDietaryPreference/{customerId}/{dietaryPreferenceId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteDietaryPreference(int customerId, int dietaryPreferenceId, CancellationToken cancellation)
        {
            if (customerId <= 0 || dietaryPreferenceId <= 0)
            {
                _logger.LogWarning("Invalid customerId or dietaryPreferenceId provided for deletion. CustomerId: {customerId}, DietaryPreferenceId: {dietaryPreferenceId}.", customerId, dietaryPreferenceId);
                return BadRequest(SBResponse<bool>.Failure("Invalid customerId and dietaryPreferenceId. Both must be positive integers.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietaryPreferenceService.DeleteDietaryPreference(customerId, dietaryPreferenceId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting dietary preference information for CustomerId: {customerId}, DietaryPreferenceId: {dietaryPreferenceId}. Error: {errorMessage}", customerId, dietaryPreferenceId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting dietary preference information for CustomerId: {customerId}, DietaryPreferenceId: {dietaryPreferenceId}.", customerId, dietaryPreferenceId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }
    }
}
