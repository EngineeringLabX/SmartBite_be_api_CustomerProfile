using Microsoft.AspNetCore.Mvc;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;

namespace SmartBite.CustomerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietPatternController(IDietPatternService dietPatternService, ILogger<DietPatternController> logger) : ControllerBase
    {
        private readonly IDietPatternService _dietPatternService = dietPatternService;
        private readonly ILogger<DietPatternController> _logger = logger;

        [HttpGet("GetAllDietPatterns")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<DietPatternDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<DietPatternDto>?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<List<DietPatternDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<DietPatternDto>?>>> GetAllDietPatterns(CancellationToken cancellation)
        {
            try
            {
                var result = await _dietPatternService.GetAllDietPatterns(cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching all diet pattern information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all diet pattern information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<List<DietPatternDto>?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("GetDietPattern/{dietPatternId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<DietPatternDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<DietPatternDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<DietPatternDto?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<DietPatternDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<DietPatternDto?>>> GetDietPattern(int dietPatternId, CancellationToken cancellation)
        {
            if (dietPatternId <= 0)
            {
                _logger.LogWarning("Invalid dietPatternId provided: {dietPatternId}. It must be a positive integer.", dietPatternId);
                return BadRequest(SBResponse<DietPatternDto?>.Failure("Invalid dietPatternId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietPatternService.GetDietPattern(dietPatternId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching diet pattern information for DietPatternId: {dietPatternId}. Error: {errorMessage}", dietPatternId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching diet pattern information for DietPatternId: {dietPatternId}.", dietPatternId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<DietPatternDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("CreateDietPattern")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<DietPatternDto?>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SBResponse<DietPatternDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<DietPatternDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<DietPatternDto?>>> CreateDietPattern(DietPatternDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid DietPatternDto for creation.");
                return BadRequest(SBResponse<DietPatternDto?>.Failure("DietPatternDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietPatternService.CreateDietPattern(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while creating diet pattern information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating diet pattern information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<DietPatternDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("UpdateDietPattern")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> UpdateDietPattern(DietPatternDto dto, CancellationToken cancellation)
        {
            if (dto == null || dto.DietPatternId <= 0 || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid DietPatternDto for update.");
                return BadRequest(SBResponse<bool>.Failure("DietPatternDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietPatternService.UpdateDietPattern(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while updating diet pattern information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating diet pattern information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("DeleteDietPattern/{dietPatternId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteDietPattern(int dietPatternId, CancellationToken cancellation)
        {
            if (dietPatternId <= 0)
            {
                _logger.LogWarning("Invalid dietPatternId provided for deletion: {dietPatternId}. It must be a positive integer.", dietPatternId);
                return BadRequest(SBResponse<bool>.Failure("Invalid dietPatternId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }

            try
            {
                var result = await _dietPatternService.DeleteDietPattern(dietPatternId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting diet pattern information for DietPatternId: {dietPatternId}. Error: {errorMessage}", dietPatternId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting diet pattern information for DietPatternId: {dietPatternId}.", dietPatternId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }
    }
}
