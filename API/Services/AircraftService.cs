using API.Models;
using API.Models.Request;
using API.Repositories;
using Microsoft.Extensions.ObjectPool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAircraftImageRepository _aircraftImageRepository;
        private readonly IAircraftDocumentRepository _aircraftDocumentRepository;
        private readonly IAircraftModelRepository _aircraftModelRepository;

        public AircraftService(IAircraftRepository aircraftRepository, IAircraftImageRepository aircraftImageRepository, IAircraftDocumentRepository aircraftDocumentRepository, IAircraftModelRepository aircraftModelRepository)
        {
            _aircraftRepository = aircraftRepository;
            _aircraftImageRepository = aircraftImageRepository;
            _aircraftDocumentRepository = aircraftDocumentRepository;
            _aircraftModelRepository = aircraftModelRepository;
        }

        #region Aircraft

        public async Task<IEnumerable<Aircraft>> GetAllAsync()
        {
            return await _aircraftRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<AircraftSummary>, int)> GetAllActiveAsync(int page, int pageSize, string? query, string? category, string? manufacturer, string? model, int? year)
        {
            return await _aircraftRepository.GetAllActiveAsync(page, pageSize, query, category, manufacturer, model, year);
        }

        public async Task<IEnumerable<AircraftSummary>> GetAllFeaturedAsync()
        {
            var featuredAircraft = await _aircraftRepository.GetAllFeaturedAsync();

            return featuredAircraft;
        }

        public async Task<IEnumerable<AircraftModel>> GetAllActiveModelsAsync()
        {
            return await _aircraftModelRepository.GetAllActiveAsync();
        }

        public async Task<Aircraft?> GetByIdAsync(long id)
        {
            var aircraft = await _aircraftRepository.GetByIdAsync(id);

            if (aircraft != null)
            {
                aircraft.Images = await _aircraftImageRepository.GetByAircraftIdAsync(id);
                aircraft.Documents = await _aircraftDocumentRepository.GetByAircraftIdAsync(id);
            }

            return aircraft;
        }

        public async Task<Aircraft?> SaveAsync(AircraftSaveRequest aircraftRequest)
        {
            var aircraftSell = new AircraftSell
            {
                AircraftId = aircraftRequest.AircraftId,
                Type = aircraftRequest.Type,
                Category = aircraftRequest.Category,
                Manufacturer = aircraftRequest.Manufacturer,
                Condition = aircraftRequest.Condition,
                Seats = aircraftRequest.Seats,
                Fuel = aircraftRequest.Fuel,
                Reference = aircraftRequest.Reference,
                TailNumber = aircraftRequest.TailNumber,
                Status = aircraftRequest.Status,
                Country = aircraftRequest.Country,
                TotalHours = aircraftRequest.TotalHours,
                Currency = aircraftRequest.Currency,
                ShowPrice = aircraftRequest.ShowPrice,
                Featured = aircraftRequest.Featured,
                FakeDelete = aircraftRequest.FakeDelete,
                ModelId = aircraftRequest.ModelId,
                Year = aircraftRequest.Year,
                Price = aircraftRequest.Price,
                TechnicalFile = aircraftRequest.TechnicalFile,
                PostedDate = aircraftRequest.PostedDate
            };

            if (aircraftSell.AircraftId == 0)
            {
                aircraftSell.AircraftId = generateUniqueId();
                aircraftSell.PostedDate = DateTime.UtcNow;
                aircraftSell.UpdateDate = DateTime.UtcNow;

                await AddAsync(aircraftSell);
            }
            else
            {
                aircraftSell.UpdateDate = DateTime.UtcNow;

                await UpdateAsync(aircraftSell);
            }

            if (aircraftRequest.Images != null && aircraftRequest.Images.Count > 0)
            {
                foreach (var image in aircraftRequest.Images)
                {
                    image.AircraftId = aircraftSell.AircraftId;
                }

                await SaveImagesAsync(aircraftRequest.Images);
            }

            if (aircraftRequest.Documents != null && aircraftRequest.Documents.Count > 0)
            {
                foreach (var document in aircraftRequest.Documents)
                {
                    document.AircraftId = aircraftSell.AircraftId;
                }

                await SaveDocumentsAsync(aircraftRequest.Documents);
            }

            return await GetByIdAsync(aircraftSell.AircraftId);
        }

        public async Task AddAsync(AircraftSell aircraft)
        {
            await _aircraftRepository.AddAsync(aircraft);
        }

        public async Task UpdateAsync(AircraftSell aircraft)
        {
            await _aircraftRepository.UpdateAsync(aircraft);
        }

        public async Task DeleteAsync(long id)
        {
            await _aircraftRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SearchFilter>> GetSearchFiltersAsync()
        {
            return await _aircraftRepository.GetSearchFiltersAsync();
        }

        public async Task<string[]> GetCategoriesAsync()
        {
            return await _aircraftRepository.GetCategoriesAsync();
        }

        public async Task<string[]> GetManufacturersByCategoryAsync(string category)
        {
            return await _aircraftRepository.GetManufacturersByCategoryAsync(category);
        }

        public async Task<string[]> GetModelsByCategoryAndManufacturerAsync(string category, string manufacturer)
        {
            return await _aircraftRepository.GetModelsByCategoryAndManufacturerAsync(category, manufacturer);
        }

        public async Task<int[]> GetYearsByCategoryManufacturerAndModelAsync(string category, string manufacturer, string model)
        {
            return await _aircraftRepository.GetYearsByCategoryManufacturerAndModelAsync(category, manufacturer, model);
        }

        #endregion

        #region Aircraft Images

        public async Task<IEnumerable<AircraftImage>> GetAllImagesAsync()
        {
            return await _aircraftImageRepository.GetAllAsync();
        }

        public async Task<AircraftImage?> GetImageByIdAsync(long id)
        {
            return await _aircraftImageRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AircraftImage>> GetImagesByAircraftIdAsync(long aircraftId)
        {
            return await _aircraftImageRepository.GetByAircraftIdAsync(aircraftId);
        }

        public async Task SaveImagesAsync(List<AircraftImageRequest> aircraftImages)
        {
            if (aircraftImages == null || aircraftImages.Count == 0)
                return;

            var existingImages = await GetImagesByAircraftIdAsync(aircraftImages[0].AircraftId);

            foreach (var aircraftImage in aircraftImages)
            {
                if (aircraftImage.ImageId == 0)
                {
                    aircraftImage.ImageId = generateUniqueId();

                    await _aircraftImageRepository.SaveAsync(new AircraftImage
                    {
                        ImageId = aircraftImage.ImageId,
                        AircraftId = aircraftImage.AircraftId,
                        ImageBase64 = aircraftImage.ImageBase64,
                        Description = aircraftImage.Description,
                        Path = aircraftImage.Path,
                        IsMain = aircraftImage.IsMain
                    });
                }
            }

            var imagesToDelete = existingImages.Where(existingImage =>
                !aircraftImages.Any(ai => ai.ImageId == existingImage.ImageId)).ToList();

            foreach (var imageToDelete in imagesToDelete)
            {
                await _aircraftImageRepository.DeleteAsync(imageToDelete.ImageId);
            }
        }

        public async Task DeleteImageAsync(long id)
        {
            await _aircraftImageRepository.DeleteAsync(id);
        }

        #endregion

        #region Aircraft Documents

        public async Task<IEnumerable<AircraftDocument>> GetAllDocumentsAsync()
        {
            return await _aircraftDocumentRepository.GetAllAsync();
        }

        public async Task<AircraftDocument?> GetDocumentByIdAsync(long id)
        {
            return await _aircraftDocumentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AircraftDocument>> GetDocumentsByAircraftIdAsync(long aircraftId)
        {
            return await _aircraftDocumentRepository.GetByAircraftIdAsync(aircraftId);
        }

        public async Task SaveDocumentsAsync(List<AircraftDocumentRequest> aircraftDocuments)
        {
            if (aircraftDocuments == null || aircraftDocuments.Count == 0)
                return;

            var existingDocuments = await GetDocumentsByAircraftIdAsync(aircraftDocuments[0].AircraftId);

            foreach (var document in aircraftDocuments)
            {
                if (document.DocumentId == 0)
                {
                    await _aircraftDocumentRepository.AddAsync(new AircraftDocument
                    {
                        DocumentId = document.DocumentId,
                        AircraftId = document.AircraftId,
                        Description = document.Description,
                        DocumentBase64 = document.DocumentBase64,
                        Date = DateTime.Now
                    });
                }
            }

            var documentsToDelete = existingDocuments.Where(existingDocument =>
                !aircraftDocuments.Any(ad => ad.DocumentId == existingDocument.DocumentId)).ToList();

            foreach (var documentToDelete in documentsToDelete)
            {
                await _aircraftDocumentRepository.DeleteAsync(documentToDelete.DocumentId);
            }
        }

        public async Task DeleteDocumentAsync(long id)
        {
            await _aircraftDocumentRepository.DeleteAsync(id);
        }

        #endregion

        #region Private Methods

        private static long generateUniqueId()
        {
            byte[] bytes = new byte[8];
            System.Security.Cryptography.RandomNumberGenerator.Fill(bytes);
            long randomLong = BitConverter.ToInt64(bytes, 0) % 1_000_000_000_000_00L;
            randomLong = randomLong < 0 ? -randomLong : randomLong;

            return randomLong == 0 ? 1 : randomLong;
        }

        private static string ToTitleCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        
        #endregion
    }
}
