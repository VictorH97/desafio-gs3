using API.Services;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Models.Request;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AircraftController : ControllerBase
    {
        private readonly IAircraftService _aircraftService;

        public AircraftController(IAircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        #region Admin

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var aircraft = await _aircraftService.GetAllAsync();
            return Ok(aircraft);
        }

        [HttpGet("active-models")]
        [Authorize]
        public async Task<IActionResult> GetAllActiveModels()
        {
            var models = await _aircraftService.GetAllActiveModelsAsync();
            return Ok(models);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save([FromBody] AircraftSaveRequest aircraftSaveRequest)
        {
            var aircraft = await _aircraftService.SaveAsync(aircraftSaveRequest);
            return Ok(aircraft);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] AircraftSell aircraft)
        {
            await _aircraftService.UpdateAsync(aircraft);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            await _aircraftService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id}/images")]
        [Authorize]
        public async Task<IActionResult> GetImagesByAircraftId(long id)
        {
            var images = await _aircraftService.GetImagesByAircraftIdAsync(id);
            if (images == null) return NotFound();
            return Ok(images);
        }

        [HttpPost("images")]
        [Authorize]
        public async Task<IActionResult> AddImage([FromBody] List<AircraftImageRequest> aircraftImages)
        {
            await _aircraftService.SaveImagesAsync(aircraftImages);
            return Ok();
        }

        #endregion

        #region Public

        [HttpGet]
        public async Task<IActionResult> GetAllActive(int page = 1, int pageSize = 10, string? query = null, string? category = null, string? manufacturer = null, string? model = null, int? year = null)
        {
            var result = await _aircraftService.GetAllActiveAsync(page, pageSize, query, category, manufacturer, model, year);
            return Ok(new { items = result.Item1, totalItems = result.Item2 });
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetAllFeatured()
        {
            var aircraft = await _aircraftService.GetAllFeaturedAsync();
            return Ok(aircraft);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var aircraft = await _aircraftService.GetByIdAsync(id);
            if (aircraft == null) return NotFound();
            return Ok(aircraft);
        }

        [HttpGet("search-filters")]
        public async Task<IActionResult> GetSearchFilters()
        {
            var filters = await _aircraftService.GetSearchFiltersAsync();
            return Ok(filters);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _aircraftService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("manufacturers")]
        public async Task<IActionResult> GetManufacturersByCategory([FromQuery] string category)
        {
            var manufacturers = await _aircraftService.GetManufacturersByCategoryAsync(category);
            return Ok(manufacturers);
        }

        [HttpGet("models")]
        public async Task<IActionResult> GetModelsByCategoryAndManufacturer([FromQuery] string category, [FromQuery] string manufacturer)
        {
            var models = await _aircraftService.GetModelsByCategoryAndManufacturerAsync(category, manufacturer);
            return Ok(models);
        }
        
        [HttpGet("years")]
        public async Task<IActionResult> GetYearsByCategoryManufacturerAndModel([FromQuery] string category, [FromQuery] string manufacturer, [FromQuery] string model)
        {
            var years = await _aircraftService.GetYearsByCategoryManufacturerAndModelAsync(category, manufacturer, model);
            return Ok(years);
        }

        #endregion
    }
}
