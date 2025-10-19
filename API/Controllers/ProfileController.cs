using API.Models.Request;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        // private readonly ProfileService _profileService;

        // public ProfileController(ProfileService profileService)
        // {
        //     _profileService = profileService;
        // }

        // [HttpGet("profiles")]
        // public async Task<IActionResult> GetProfiles()
        // {
        //     try
        //     {
        //         var profiles = await _profileService.GetAllAsync();
        //         return Ok(profiles);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = ex.Message });
        //     }
        // }

        // [HttpGet("profiles/{id}")]
        // public async Task<IActionResult> GetProfileById(int id)
        // {
        //     try
        //     {
        //         var profile = await _profileService.GetByIdAsync(id);
        //         return Ok(profile);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = ex.Message });
        //     }
        // }

        // [HttpPost("profiles")]
        // public async Task<IActionResult> CreateProfile([FromBody] CreateProfileRequest request)
        // {
        //     try
        //     {
        //         if (string.IsNullOrWhiteSpace(request.Name))
        //         {
        //             return BadRequest(new { message = "Nome é obrigatório" });
        //         }

        //         var profile = await _profileService.CreateAsync(request.Name);
        //         return CreatedAtAction(nameof(GetProfileById), new { id = profile.Id }, profile);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = ex.Message });
        //     }
        // }

        // [HttpPut("profiles/{id}")]
        // public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileRequest request)
        // {
        //     try
        //     {
        //         if (string.IsNullOrWhiteSpace(request.Name))
        //         {
        //             return BadRequest(new { message = "Nome é obrigatório" });
        //         }

        //         await _profileService.UpdateAsync(id, request.Name);
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = ex.Message });
        //     }
        // }

        // [HttpDelete("profiles/{id}")]
        // public async Task<IActionResult> DeleteProfile(int id)
        // {
        //     try
        //     {
        //         await _profileService.DeleteAsync(id);
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = ex.Message });
        //     }
        // }
    }
}