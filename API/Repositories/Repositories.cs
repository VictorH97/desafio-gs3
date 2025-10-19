using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    #region UserRepository
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region AircraftRepository

    public class AircraftRepository : IAircraftRepository
    {
        private readonly AppDbContext _context;
        public AircraftRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aircraft>> GetAllAsync()
        {
            var result = from aircraftSell in _context.AircraftSell
                         join aircraftModel in _context.AircraftModel
                         on aircraftSell.ModelId equals aircraftModel.ModelId
                         select new Aircraft
                         {
                             Id = aircraftSell.AircraftId,
                             Reference = aircraftSell.Reference ?? string.Empty,
                             TailNumber = aircraftSell.TailNumber ?? string.Empty,
                             Type = aircraftSell.Type ?? string.Empty,
                             Category = aircraftSell.Category ?? string.Empty,
                             ModelId = aircraftModel.ModelId,
                             ModelName = aircraftModel.Name,
                             Manufacturer = aircraftModel.Manufacturer,
                             Year = aircraftSell.Year,
                             Price = aircraftSell.Price,
                             Country = aircraftSell.Country,
                             Condition = aircraftSell.Condition ?? string.Empty,
                             Fuel = aircraftSell.Fuel ?? string.Empty,
                             PostedDate = aircraftSell.PostedDate,
                             Seats = aircraftSell.Seats,
                             Status = aircraftSell.Status ?? string.Empty,
                             TotalHours = aircraftSell.TotalHours,
                             Featured = Convert.ToBoolean(aircraftSell.Featured ?? 0)
                         };

            return await result.ToListAsync();
        }

        public async Task<(IEnumerable<AircraftSummary>, int)> GetAllActiveAsync(int page, int pageSize, string? query, string? category, string? manufacturer, string? model, int? year)
        {
            var result = from aircraftSell in _context.AircraftSell
                         join aircraftModel in _context.AircraftModel
                         on aircraftSell.ModelId equals aircraftModel.ModelId
                         join image in _context.AircraftImage
                         on aircraftSell.AircraftId equals image.AircraftId into aircraftImages
                         from image in aircraftImages.DefaultIfEmpty()
                         where aircraftSell.FakeDelete == 0 && 
                               (image.IsMain == "Sim" || image == null) &&
                               (string.IsNullOrEmpty(query) || 
                                (aircraftSell.Reference != null && aircraftSell.Reference.Contains(query)) || 
                                (aircraftModel.Name != null && aircraftModel.Name.Contains(query))) &&
                               (string.IsNullOrEmpty(category) || (aircraftSell.Category != null && aircraftSell.Category.ToLower().TrimEnd() == category.ToLower().TrimEnd())) &&
                               (string.IsNullOrEmpty(manufacturer) || (aircraftModel.Manufacturer != null && aircraftModel.Manufacturer.ToLower().TrimEnd() == manufacturer.ToLower().TrimEnd())) &&
                               (string.IsNullOrEmpty(model) || (aircraftModel.Name != null && aircraftModel.Name.ToLower().TrimEnd() == model.ToLower().TrimEnd())) &&
                               (!year.HasValue || aircraftSell.Year == year.Value)
                         orderby aircraftSell.AircraftId
                         select new AircraftSummary
                         {
                             Id = aircraftSell.AircraftId,
                             Reference = aircraftSell.Reference ?? string.Empty,
                             Category = ToTitleCase(aircraftSell.Category ?? string.Empty),
                             Manufacturer = ToTitleCase(aircraftModel.Manufacturer ?? string.Empty),
                             ModelName = ToTitleCase(aircraftModel.Name ?? string.Empty),
                             Year = aircraftSell.Year,
                             Seats = aircraftSell.Seats,
                             Engine = aircraftSell.Engine ?? string.Empty,
                             Fuel = aircraftSell.Fuel ?? string.Empty,
                             TotalHours = aircraftSell.TotalHours,
                             MainImage = image
                         };

            var totalItems = await result.CountAsync();

            result = result.Skip((page - 1) * pageSize).Take(pageSize);

            return (await result.ToListAsync(), totalItems);
        }

        public async Task<IEnumerable<AircraftSummary>> GetAllFeaturedAsync()
        {
            var result = from aircraftSell in _context.AircraftSell
                         join aircraftModel in _context.AircraftModel
                         on aircraftSell.ModelId equals aircraftModel.ModelId
                         join image in _context.AircraftImage
                         on aircraftSell.AircraftId equals image.AircraftId into aircraftImages
                         from image in aircraftImages.DefaultIfEmpty()
                         where aircraftSell.Featured == 1 && aircraftSell.FakeDelete == 0 && (image.IsMain == "Sim" || image == null)
                         select new AircraftSummary
                         {
                             Id = aircraftSell.AircraftId,
                             Reference = aircraftSell.Reference ?? string.Empty,
                             Category = aircraftSell.Category ?? string.Empty,
                             Manufacturer = aircraftModel.Manufacturer,
                             ModelName = aircraftModel.Name,
                             Year = aircraftSell.Year,
                             Seats = aircraftSell.Seats,
                             Engine = aircraftSell.Engine ?? string.Empty,
                             Fuel = aircraftSell.Fuel ?? string.Empty,
                             TotalHours = aircraftSell.TotalHours,
                             MainImage = image
                         };

            return await result.ToListAsync();
        }

        public async Task<Aircraft?> GetByIdAsync(long id)
        {
            var result = from aircraftSell in _context.AircraftSell
                         join aircraftModel in _context.AircraftModel
                         on aircraftSell.ModelId equals aircraftModel.ModelId
                         where aircraftSell.AircraftId == id
                         select new Aircraft
                         {
                             Id = aircraftSell.AircraftId,
                             Reference = aircraftSell.Reference ?? string.Empty,
                             TailNumber = aircraftSell.TailNumber ?? string.Empty,
                             Type = aircraftSell.Type ?? string.Empty,
                             Category = aircraftSell.Category ?? string.Empty,
                             ModelId = aircraftModel.ModelId,
                             ModelName = aircraftModel.Name,
                             Manufacturer = aircraftModel.Manufacturer,
                             Year = aircraftSell.Year,
                             Price = aircraftSell.Price,
                             Currency = aircraftSell.Currency ?? string.Empty,
                             ShowPrice = Convert.ToBoolean(aircraftSell.ShowPrice ?? 0),
                             Condition = aircraftSell.Condition ?? string.Empty,
                             Fuel = aircraftSell.Fuel ?? string.Empty,
                             PostedDate = aircraftSell.PostedDate,
                             Seats = aircraftSell.Seats,
                             Status = aircraftSell.Status ?? string.Empty,
                             TotalHours = aircraftSell.TotalHours,
                             Featured = Convert.ToBoolean(aircraftSell.Featured ?? 0)
                         };

            return await result.FirstOrDefaultAsync();
        }

        public async Task AddAsync(AircraftSell aircraft)
        {
            _context.AircraftSell.Add(aircraft);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AircraftSell aircraft)
        {
            _context.AircraftSell.Update(aircraft);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var aircraft = await _context.AircraftSell.FindAsync(id);
            if (aircraft != null)
            {
                aircraft.FakeDelete = 1;
                aircraft.Status = "Supenso de venda";

                _context.AircraftSell.Update(aircraft);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SearchFilter>> GetSearchFiltersAsync()
        {
            var result = from aircraftSell in _context.AircraftSell
                         join aircraftModel in _context.AircraftModel
                         on aircraftSell.ModelId equals aircraftModel.ModelId
                         where aircraftSell.FakeDelete == 0
                         select new SearchFilter
                         {
                             Type = aircraftSell.Type ?? string.Empty,
                             Category = aircraftSell.Category ?? string.Empty,
                             ModelName = aircraftModel.Name,
                             Manufacturer = aircraftModel.Manufacturer,
                             Year = aircraftSell.Year ?? 0,
                         };

            return await result.ToListAsync();
        }

        public async Task<string[]> GetCategoriesAsync()
        {
            return await _context.AircraftSell
                .Where(a => a.FakeDelete == 0 && !string.IsNullOrEmpty(a.Category))
                .Select(a => ToTitleCase(a.Category ?? string.Empty))
                .Distinct()
                .ToArrayAsync();
        }

        public async Task<string[]> GetManufacturersByCategoryAsync(string category)
        {
            var query = from aircraftSell in _context.AircraftSell
                        join aircraftModel in _context.AircraftModel
                        on aircraftSell.ModelId equals aircraftModel.ModelId
                        where aircraftSell.FakeDelete == 0
                        && aircraftSell.Category != null && aircraftSell.Category == category
                        select ToTitleCase(aircraftModel.Manufacturer ?? string.Empty);

            return await query.Distinct().ToArrayAsync();
        }

        public async Task<string[]> GetModelsByCategoryAndManufacturerAsync(string category, string manufacturer)
        {
            var query = from aircraftSell in _context.AircraftSell
                        join aircraftModel in _context.AircraftModel
                        on aircraftSell.ModelId equals aircraftModel.ModelId
                        where aircraftSell.FakeDelete == 0
                        && aircraftSell.Category != null && aircraftSell.Category == category
                        && aircraftModel.Manufacturer != null && aircraftModel.Manufacturer == manufacturer
                        select ToTitleCase(aircraftModel.Name ?? string.Empty);

            return await query.Distinct().ToArrayAsync();
        }

        public async Task<int[]> GetYearsByCategoryManufacturerAndModelAsync(string category, string manufacturer, string model)
        {
            var query = from aircraftSell in _context.AircraftSell
                        join aircraftModel in _context.AircraftModel
                        on aircraftSell.ModelId equals aircraftModel.ModelId
                        where aircraftSell.FakeDelete == 0
                        && aircraftSell.Category != null && aircraftSell.Category == category
                        && aircraftModel.Manufacturer != null && aircraftModel.Manufacturer == manufacturer
                        && aircraftModel.Name != null && aircraftModel.Name == model
                        select aircraftSell.Year ?? 0;

            return await query.Distinct().ToArrayAsync();
        }
        
        private static string ToTitleCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower().TrimEnd());
        }
    }

    #endregion

    #region AircraftModelRepository
    public class AircraftModelRepository : IAircraftModelRepository
    {
        private readonly AppDbContext _context;
        public AircraftModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AircraftModel>> GetAllAsync()
        {
            return await _context.AircraftModel.ToListAsync();
        }

        public async Task<IEnumerable<AircraftModel>> GetAllActiveAsync()
        {
            return await _context.AircraftModel.Where(a => a.FakeDelete == 0).ToListAsync();
        }

        public async Task<AircraftModel?> GetByIdAsync(long id)
        {
            return await _context.AircraftModel.FindAsync(id);
        }

        public async Task AddAsync(AircraftModel model)
        {
            _context.AircraftModel.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AircraftModel model)
        {
            _context.AircraftModel.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var model = await _context.AircraftModel.FindAsync(id);
            if (model != null)
            {
                model.FakeDelete = 1;

                _context.AircraftModel.Update(model);
                await _context.SaveChangesAsync();
            }
        }
    }

    #endregion

    #region AircraftImageRepository
    public class AircraftImageRepository : IAircraftImageRepository
    {
        private readonly AppDbContext _context;

        public AircraftImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AircraftImage>> GetAllAsync()
        {
            return await _context.AircraftImage.ToListAsync();
        }

        public async Task<AircraftImage?> GetByIdAsync(long id)
        {
            return await _context.AircraftImage.FindAsync(id);
        }

        public async Task<IEnumerable<AircraftImage>> GetByAircraftIdAsync(long aircraftId)
        {
            return await _context.AircraftImage.Where(i => i.AircraftId == aircraftId).ToListAsync();
        }

        public async Task SaveAsync(AircraftImage aircraftImage)
        {
            _context.AircraftImage.Add(aircraftImage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AircraftImage aircraftImage)
        {
            _context.AircraftImage.Update(aircraftImage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var image = await _context.AircraftImage.FindAsync(id);

            if (image != null)
            {
                _context.AircraftImage.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }

    #endregion

    #region AircraftDocumentRepository

    public class AircraftDocumentRepository : IAircraftDocumentRepository
    {
        private readonly AppDbContext _context;

        public AircraftDocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AircraftDocument>> GetAllAsync()
        {
            return await _context.AircraftDocument.ToListAsync();
        }

        public async Task<AircraftDocument?> GetByIdAsync(long id)
        {
            return await _context.AircraftDocument.FindAsync(id);
        }

        public async Task<IEnumerable<AircraftDocument>> GetByAircraftIdAsync(long aircraftId)
        {
            return await _context.AircraftDocument.Where(i => i.AircraftId == aircraftId).ToListAsync();
        }

        public async Task AddAsync(AircraftDocument aircraftDocument)
        {
            _context.AircraftDocument.Add(aircraftDocument);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AircraftDocument aircraftDocument)
        {
            _context.AircraftDocument.Update(aircraftDocument);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var document = await _context.AircraftDocument.FindAsync(id);

            if (document != null)
            {
                _context.AircraftDocument.Remove(document);
                await _context.SaveChangesAsync();
            }
        }

    }

    #endregion

    #region ObservationRepository

    public class ObservationRepository : IObservationRepository
    {
        private readonly AppDbContext _context;

        public ObservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Observation>> GetByAircraftIdAsync(long aircraftId)
        {
            return await _context.Observation
                .Where(o => o.AircraftId == aircraftId && o.FakeDelete == 0)
                .OrderByDescending(o => o.Date)
                .ToListAsync();
        }

        public async Task<Observation?> GetByIdAsync(long id)
        {
            return await _context.Observation
                .Where(o => o.Id == id && o.FakeDelete == 0)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Observation observation)
        {
            observation.FakeDelete = 0;
            _context.Observation.Add(observation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Observation observation)
        {
            _context.Observation.Update(observation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var observation = await _context.Observation.FindAsync(id);
            if (observation != null)
            {
                observation.FakeDelete = 1;
                _context.Observation.Update(observation);
                await _context.SaveChangesAsync();
            }
        }
    }

    #endregion
}