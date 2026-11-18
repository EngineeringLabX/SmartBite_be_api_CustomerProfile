using Microsoft.AspNetCore.Mvc;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;

namespace SmartBite.CustomerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController(IAllergyService allergyService, ILogger<AllergyController> logger) : ControllerBase
    {
        private readonly IAllergyService _allergyService = allergyService;
        private readonly ILogger<AllergyController> _logger = logger;

        [HttpGet("GetAllAllergy")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<AllergyDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<AllergyDto>?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<List<AllergyDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<AllergyDto>?>>> GetAllAllergy(CancellationToken cancellation)
        {
            try
            {
                var result = await _allergyService.GetAllAllergy(cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching all allergy information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all allergy information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<List<AllergyDto>?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("GetAllergy/{allergyId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<AllergyDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<AllergyDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<AllergyDto?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<AllergyDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<AllergyDto?>>> GetAllergy(int allergyId, CancellationToken cancellation)
        {
            if (allergyId <= 0)
            {
                _logger.LogWarning("Invalid allergyId provided: {allergyId}. It must be a positive integer.", allergyId);
                return BadRequest(SBResponse<AllergyDto?>.Failure("Invalid allergyId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergyService.GetAllergy(allergyId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching allergy information for AllergyId: {allergyId}. Error: {errorMessage}", allergyId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching allergy information for AllergyId: {allergyId}.", allergyId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<AllergyDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("CreateAllergy")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<AllergyDto?>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SBResponse<AllergyDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<AllergyDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<AllergyDto?>>> CreateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid AllergyDto for creation.");
                return BadRequest(SBResponse<AllergyDto?>.Failure("AllergyDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergyService.CreateAllergy(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while creating allergy information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating allergy information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<AllergyDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("UpdateAllergy")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> UpdateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid AllergyDto for update.");
                return BadRequest(SBResponse<bool>.Failure("AllergyDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergyService.UpdateAllergy(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while updating allergy information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating allergy information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("DeleteAllergy/{allergyId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteAllergy(int allergyId, CancellationToken cancellation)
        {
            if (allergyId <= 0)
            {
                _logger.LogWarning("Invalid allergyId provided for deletion: {allergyId}. It must be a positive integer.", allergyId);
                return BadRequest(SBResponse<bool>.Failure("Invalid allergyId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _allergyService.DeleteAllergy(allergyId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting allergy information for AllergyId: {allergyId}. Error: {errorMessage}", allergyId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting allergy information for AllergyId: {allergyId}.", allergyId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }
    }
}
