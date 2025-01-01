using HUUTRUNG.Models.Domain;


namespace HUUTRUNG_WEBAPI.Repositories
{
    public interface ISeriesAPIRepository
    {
       public Task<List<Series>> GetAllAsync(); 
       public Task<Series?> GetByIdAsync(int id);
       public Task<Series> CreateAsync(Series series);
       public Task<Series> UpdateAsync(int id,Series series);
       public Task<Series?> DeleteAsync(int id);
    }
}
