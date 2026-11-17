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

        [HttpGet("GetAllAllergenRestrictions")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<AllergenRestrictionDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<AllergenRestrictionDto>?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<List<AllergenRestrictionDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<AllergenRestrictionDto>?>>> GetAllAllergenRestrictions(CancellationToken cancellation)
        {
            try
            {
                var result = await _allergenRestrictionService.GetAllAllergenRestrictions(cancellation);
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

        [HttpGet("GetAllergenRestriction/{allergenRestrictionId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<AllergenRestrictionDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<AllergenRestrictionDto?>>> GetAllergenRestriction(int allergenRestrictionId, CancellationToken cancellation)
        {
            if (allergenRestrictionId <= 0)
            {
                _logger.LogWarning("Invalid allergenRestrictionId provided: {allergenRestrictionId}. It must be a positive integer.", allergenRestrictionId);
                return BadRequest(SBResponse<AllergenRestrictionDto?>.Failure("Invalid allergenRestrictionId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergenRestrictionService.GetAllergenRestriction(allergenRestrictionId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching allergen restriction for AllergenRestrictionId: {allergenRestrictionId}. Error: {errorMessage}", allergenRestrictionId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching allergen restriction for AllergenRestrictionId: {allergenRestrictionId}.", allergenRestrictionId);
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
            if (dto == null || !ModelState.IsValid)
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
            if (dto == null || !ModelState.IsValid)
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

        [HttpDelete("DeleteAllergenRestriction/{allergenRestrictionId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteAllergenRestriction(int allergenRestrictionId, CancellationToken cancellation)
        {
            if (allergenRestrictionId <= 0)
            {
                _logger.LogWarning("Invalid allergenRestrictionId provided for deletion: {allergenRestrictionId}. It must be a positive integer.", allergenRestrictionId);
                return BadRequest(SBResponse<bool>.Failure("Invalid allergenRestrictionId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergenRestrictionService.DeleteAllergenRestriction(allergenRestrictionId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting allergen restriction for AllergenRestrictionId: {allergenRestrictionId}. Error: {errorMessage}", allergenRestrictionId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting allergen restriction for AllergenRestrictionId: {allergenRestrictionId}.", allergenRestrictionId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }
    }
}
