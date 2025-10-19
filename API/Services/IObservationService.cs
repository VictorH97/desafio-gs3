using API.Models;
using API.Models.Request;

namespace API.Services
{
    public interface IObservationService
    {
        Task<IEnumerable<Observation>> GetByAircraftIdAsync(long aircraftId);
        Task<Observation> AddAsync(ObservationRequest request);
        Task<Observation> UpdateAsync(ObservationUpdateRequest request);
    }
} 