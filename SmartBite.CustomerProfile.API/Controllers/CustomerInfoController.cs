using Microsoft.AspNetCore.Mvc;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;

namespace SmartBite.CustomerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerInfoController(ICustomerInfoService customerInfoService, ILogger<CustomerInfoController> logger) : ControllerBase
    {
        private readonly ICustomerInfoService _customerInfoService = customerInfoService;
        private readonly ILogger<CustomerInfoController> _logger = logger;

        [HttpGet("GetAllCustomerInfo")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<CustomerInfoDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<CustomerInfoDto>?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<List<CustomerInfoDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<CustomerInfoDto>?>>> GetAllCustomerInfo(CancellationToken cancellation)
        {
            try
            {
                var result = await _customerInfoService.GetAllCustomerInfo(cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching all customer information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all customer information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<List<CustomerInfoDto>?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("GetCustomerInfo/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<CustomerInfoDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<CustomerInfoDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<CustomerInfoDto?>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SBResponse<CustomerInfoDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<CustomerInfoDto?>>> GetCustomerInfo(int customerId, CancellationToken cancellation)
        {
            if(customerId <= 0)
            {
                _logger.LogWarning("Invalid customerId provided: {customerId}. It must be a positive integer.", customerId);
                return BadRequest(SBResponse<CustomerInfoDto?>.Failure("Invalid customerId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _customerInfoService.GetCustomerInfo(customerId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching customer information for CustomerId: {customerId}. Error: {errorMessage}", customerId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching customer information for CustomerId: {customerId}.", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<CustomerInfoDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("CreateCustomerInfo")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<CustomerInfoDto?>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SBResponse<CustomerInfoDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<CustomerInfoDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<CustomerInfoDto?>>> CreateCustomerInfo(CustomerInfoDto dto, CancellationToken cancellation)
        {
            if(dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid CustomerInfoDto for creation.");
                return BadRequest(SBResponse<CustomerInfoDto?>.Failure("CustomerInfoDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }   
            try
            {
                var result = await _customerInfoService.CreateCustomerInfo(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while creating customer information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating customer information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<CustomerInfoDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("UpdateCustomerInfo")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> UpdateCustomerInfo(CustomerInfoDto dto, CancellationToken cancellation)
        {
            if(dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid CustomerInfoDto for update.");
                return BadRequest(SBResponse<bool>.Failure("CustomerInfoDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _customerInfoService.UpdateCustomerInfo(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while updating customer information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating customer information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("DeleteCustomerInfo/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteCustomerInfo(int customerId, CancellationToken cancellation)
        {
            if (customerId <= 0)
            {
                _logger.LogWarning("Invalid customerId provided for deletion: {customerId}. It must be a positive integer.", customerId);
                return BadRequest(SBResponse<bool>.Failure("Invalid customerId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _customerInfoService.DeleteCustomerInfo(customerId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting customer information for CustomerId: {customerId}. Error: {errorMessage}", customerId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting customer information for CustomerId: {customerId}.", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

    }
}
