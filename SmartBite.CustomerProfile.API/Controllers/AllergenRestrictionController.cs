using Microsoft.AspNetCore.Mvc;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;

namespace SmartBite.CustomerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergenRestrictionController(IAllergenRestrictionService allergenRestrictionService, ILogger<AllergenRestrictionController> logger) : ControllerBase
    {
        private readonly IAllergenRestrictionService _allergenRestrictionService = allergenRestrictionService;
        private readonly ILogger<AllergenRestrictionController> _logger = logger;

        [HttpGet("GetAllAllergenRestrictions/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<AllergenRestrictionDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<AllergenRestrictionDto>?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<List<AllergenRestrictionDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<AllergenRestrictionDto>?>>> GetAllAllergenRestrictions(int customerId, CancellationToken cancellation)
        {
            if (customerId <= 0)
            {
                _logger.LogWarning("Invalid customerId provided: {customerId}. It must be a positive integer.", customerId);
                return BadRequest(SBResponse<List<AllergenRestrictionDto>?>.Failure("Invalid customerId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _allergenRestrictionService.GetAllAllergenRestrictions(customerId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching all allergen restriction information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all allergen restriction information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<List<AllergenRestrictionDto>?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("GetAllergenRestriction/{customerId}/{allergenRestrictionId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<AllergenRestrictionDto?>>> GetAllergenRestriction(int customerId, int allergenRestrictionId, CancellationToken cancellation)
        {
            if (customerId <= 0 || allergenRestrictionId <= 0)
            {
                _logger.LogWarning("Invalid customerId or allergenRestrictionId provided. CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId}.", customerId, allergenRestrictionId);
                return BadRequest(SBResponse<AllergenRestrictionDto?>.Failure("Invalid customerId and allergenRestrictionId. Both must be positive integers.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergenRestrictionService.GetAllergenRestriction(customerId, allergenRestrictionId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching allergen restriction for CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId}. Error: {errorMessage}", customerId, allergenRestrictionId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching allergen restriction for CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId}.", customerId, allergenRestrictionId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<AllergenRestrictionDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("CreateAllergenRestriction")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<AllergenRestrictionDto?>>> CreateAllergenRestriction(AllergenRestrictionDto dto, CancellationToken cancellation)
        {
            if (dto == null || dto.CustomerId <= 0 || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid AllergenRestrictionDto for creation.");
                return BadRequest(SBResponse<AllergenRestrictionDto?>.Failure("AllergenRestrictionDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergenRestrictionService.CreateAllergenRestriction(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while creating allergen restriction information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating allergen restriction information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<AllergenRestrictionDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("UpdateAllergenRestriction")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> UpdateAllergenRestriction(AllergenRestrictionDto dto, CancellationToken cancellation)
        {
            if (dto == null || dto.CustomerId <= 0 || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid AllergenRestrictionDto for update.");
                return BadRequest(SBResponse<bool>.Failure("AllergenRestrictionDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergenRestrictionService.UpdateAllergenRestriction(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while updating allergen restriction information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating allergen restriction information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("DeleteAllergenRestriction/{customerId}/{allergenRestrictionId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteAllergenRestriction(int customerId, int allergenRestrictionId, CancellationToken cancellation)
        {
            if (customerId <= 0 || allergenRestrictionId <= 0)
            {
                _logger.LogWarning("Invalid customerId or allergenRestrictionId provided for deletion. CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId}.", customerId, allergenRestrictionId);
                return BadRequest(SBResponse<bool>.Failure("Invalid customerId and allergenRestrictionId. Both must be positive integers.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergenRestrictionService.DeleteAllergenRestriction(customerId, allergenRestrictionId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting allergen restriction for CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId}. Error: {errorMessage}", customerId, allergenRestrictionId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting allergen restriction for CustomerId: {customerId}, AllergenRestrictionId: {allergenRestrictionId}.", customerId, allergenRestrictionId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }
    }
}
