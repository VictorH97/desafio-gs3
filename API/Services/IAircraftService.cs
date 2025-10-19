using API.Models;
using API.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IAircraftService
    {
        #region Airfcraft

        Task<IEnumerable<Aircraft>> GetAllAsync();
        Task<(IEnumerable<AircraftSummary>, int)> GetAllActiveAsync(int page, int pageSize, string? query, string? category, string? manufacturer, string? model, int? year);
        Task<IEnumerable<AircraftSummary>> GetAllFeaturedAsync();
        Task<IEnumerable<AircraftModel>> GetAllActiveModelsAsync();
        Task<Aircraft?> GetByIdAsync(long id);
        Task<Aircraft?> SaveAsync(AircraftSaveRequest aircraftRequest);
        Task AddAsync(AircraftSell aircraft);
        Task UpdateAsync(AircraftSell aircraft);
        Task DeleteAsync(long id);
        Task<IEnumerable<SearchFilter>> GetSearchFiltersAsync();
        Task<string[]> GetCategoriesAsync();
        Task<string[]> GetManufacturersByCategoryAsync(string category);
        Task<string[]> GetModelsByCategoryAndManufacturerAsync(string category, string manufacturer);
        Task<int[]> GetYearsByCategoryManufacturerAndModelAsync(string category, string manufacturer, string model);

        #endregion

        #region Aircraft Images

        Task<IEnumerable<AircraftImage>> GetAllImagesAsync();
        Task<AircraftImage?> GetImageByIdAsync(long id);
        Task<IEnumerable<AircraftImage>> GetImagesByAircraftIdAsync(long aircraftId);
        Task SaveImagesAsync(List<AircraftImageRequest> aircraftImages);
        Task DeleteImageAsync(long id);

        #endregion

        #region Aircraft Documents

        Task<IEnumerable<AircraftDocument>> GetAllDocumentsAsync();
        Task<AircraftDocument?> GetDocumentByIdAsync(long id);
        Task<IEnumerable<AircraftDocument>> GetDocumentsByAircraftIdAsync(long aircraftId);
        Task SaveDocumentsAsync(List<AircraftDocumentRequest> aircraftDocuments);
        Task DeleteDocumentAsync(long id);

        #endregion
    }
}
