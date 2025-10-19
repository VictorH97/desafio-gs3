using API.Models;
using API.Models.Request;
using API.Repositories;

namespace API.Services
{
    public class ObservationService : IObservationService
    {
        private readonly IObservationRepository _observationRepository;
        private readonly IAircraftRepository _aircraftRepository;

        public ObservationService(IObservationRepository observationRepository, IAircraftRepository aircraftRepository)
        {
            _observationRepository = observationRepository;
            _aircraftRepository = aircraftRepository;
        }

        public async Task<IEnumerable<Observation>> GetByAircraftIdAsync(long aircraftId)
        {
            // Verify that the aircraft exists
            var aircraft = await _aircraftRepository.GetByIdAsync(aircraftId);
            if (aircraft == null)
            {
                throw new ArgumentException($"Aircraft with ID {aircraftId} not found");
            }

            return await _observationRepository.GetByAircraftIdAsync(aircraftId);
        }

        public async Task<Observation> AddAsync(ObservationRequest request)
        {
            // Verify that the aircraft exists
            var aircraft = await _aircraftRepository.GetByIdAsync(request.AircraftId);
            if (aircraft == null)
            {
                throw new ArgumentException($"Aircraft with ID {request.AircraftId} not found");
            }

            var observation = new Observation
            {
                AircraftId = request.AircraftId,
                Date = DateTime.UtcNow,
                Description = request.Description,
                FakeDelete = 0
            };

            await _observationRepository.AddAsync(observation);
            return observation;
        }

        public async Task<Observation> UpdateAsync(ObservationUpdateRequest request)
        {
            // Get the existing observation
            var existingObservation = await _observationRepository.GetByIdAsync(request.Id);
            if (existingObservation == null)
            {
                throw new ArgumentException($"Observation with ID {request.Id} not found");
            }

            // Update the observation
            existingObservation.Date = DateTime.UtcNow;
            existingObservation.Description = request.Description;

            await _observationRepository.UpdateAsync(existingObservation);
            return existingObservation;
        }
    }
} 