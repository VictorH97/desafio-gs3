using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }

    public interface IAircraftRepository
    {
        Task<IEnumerable<Aircraft>> GetAllAsync();
        Task<(IEnumerable<AircraftSummary>, int)> GetAllActiveAsync(int page, int pageSize, string? query, string? category, string? manufacturer, string? model, int? year);
        Task<IEnumerable<AircraftSummary>> GetAllFeaturedAsync();
        Task<Aircraft?> GetByIdAsync(long id);
        Task AddAsync(AircraftSell aircraft);
        Task UpdateAsync(AircraftSell aircraft);
        Task DeleteAsync(long id);
        Task<IEnumerable<SearchFilter>> GetSearchFiltersAsync();
        Task<string[]> GetCategoriesAsync();
        Task<string[]> GetManufacturersByCategoryAsync(string category);
        Task<string[]> GetModelsByCategoryAndManufacturerAsync(string category, string manufacturer);
        Task<int[]> GetYearsByCategoryManufacturerAndModelAsync(string category, string manufacturer, string model);
    }

    public interface IAircraftModelRepository
    {
        Task<IEnumerable<AircraftModel>> GetAllAsync();
        Task<IEnumerable<AircraftModel>> GetAllActiveAsync();
        Task<AircraftModel?> GetByIdAsync(long id);
        Task AddAsync(AircraftModel aircraftModel);
        Task UpdateAsync(AircraftModel aircraftModel);
        Task DeleteAsync(long id);
    }

    public interface IAircraftImageRepository
    {
        Task<IEnumerable<AircraftImage>> GetAllAsync();
        Task<AircraftImage?> GetByIdAsync(long id);
        Task<IEnumerable<AircraftImage>> GetByAircraftIdAsync(long aircraftId);
        Task SaveAsync(AircraftImage aircraftImage);
        Task DeleteAsync(long id);
    }

    public interface IAircraftDocumentRepository
    {
        Task<IEnumerable<AircraftDocument>> GetAllAsync();
        Task<AircraftDocument?> GetByIdAsync(long id);
        Task<IEnumerable<AircraftDocument>> GetByAircraftIdAsync(long aircraftId);
        Task AddAsync(AircraftDocument aircraftDocument);
        Task DeleteAsync(long id);
    }

    public interface IObservationRepository
    {
        Task<IEnumerable<Observation>> GetByAircraftIdAsync(long aircraftId);
        Task<Observation?> GetByIdAsync(long id);
        Task AddAsync(Observation observation);
        Task UpdateAsync(Observation observation);
        Task DeleteAsync(long id);
    }
}
