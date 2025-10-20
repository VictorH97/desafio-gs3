using API.Models.Request;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public ProfileController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetProfiles()
        {
            try
            {
                var profiles = await _perfilService.GetAllAsync();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            try
            {
                var profile = await _perfilService.GetByIdAsync(id);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreatePerfilRequest request)
        {
            try
            {
                var profile = await _perfilService.CreateAsync(request);
                return CreatedAtAction(nameof(GetProfileById), new { id = profile.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdatePerfilRequest request)
        {
            try
            {
                await _perfilService.UpdateAsync(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            try
            {
                await _perfilService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}