using Microsoft.AspNetCore.Mvc;
using SmartBite.CustomerProfile.Application.Common.Wrapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Application.Interfaces;
using System.Net;

namespace SmartBite.CustomerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController(IAllergyService allergyService, ILogger<AllergyController> logger) : ControllerBase
    {
        private readonly IAllergyService _allergyService = allergyService;
        private readonly ILogger<AllergyController> _logger = logger;

        [HttpGet("GetAllAllergy")]
        public async Task<ActionResult<SBResponse<List<AllergyDto>?>>> GetAllAllergy(CancellationToken cancellation)
        {
            var result = await _allergyService.GetAllAllergy(cancellation);
            return result.IsSuccess ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetAllergy/{allergyId}")]
        public async Task<ActionResult<SBResponse<AllergyDto?>>> GetAllergy(int allergyId, CancellationToken cancellation)
        {
            if (allergyId <= 0) return BadRequest(SBResponse<AllergyDto?>.Failure("Invalid allergyId. It must be a positive integer.", HttpStatusCode.BadRequest));
            var result = await _allergyService.GetAllergy(allergyId, cancellation);
            return result.IsSuccess ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("CreateAllergy")]
        public async Task<ActionResult<SBResponse<AllergyDto?>>> CreateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(SBResponse<AllergyDto?>.Failure("AllergyDto is invalid.", HttpStatusCode.BadRequest));
            var result = await _allergyService.CreateAllergy(dto, cancellation);
            return result.IsSuccess ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("UpdateAllergy")]
        public async Task<ActionResult<SBResponse<bool>>> UpdateAllergy(AllergyDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(SBResponse<bool>.Failure("AllergyDto is invalid.", HttpStatusCode.BadRequest));
            var result = await _allergyService.UpdateAllergy(dto, cancellation);
            return result.IsSuccess ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("DeleteAllergy/{allergyId}")]
        public async Task<ActionResult<SBResponse<bool>>> DeleteAllergy(int allergyId, CancellationToken cancellation)
        {
            if (allergyId <= 0) return BadRequest(SBResponse<bool>.Failure("Invalid allergyId. It must be a positive integer.", HttpStatusCode.BadRequest));
            var result = await _allergyService.DeleteAllergy(allergyId, cancellation);
            return result.IsSuccess ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
    }
}
