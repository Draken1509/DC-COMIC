using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;

using Microsoft.EntityFrameworkCore;

namespace HUUTRUNG_WEBAPI.Repositories
{
    public class SQLSeriesRepository : ISeriesAPIRepository
    {
		#region khong su dung
		private readonly ApplicationDbContext _dbContext;
		public SQLSeriesRepository(ApplicationDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<Series> CreateAsync(Series series)
		{
			await _dbContext.Series.AddAsync(series);
			await _dbContext.SaveChangesAsync();
			return series;
		}
		public async Task<Series> DeleteAsync(int id)
		{
			var exstingSeries = await _dbContext.Series.FirstOrDefaultAsync(s => s.Id == id);
			if (exstingSeries == null)
			{
				return null;
			}
			_dbContext.Series.Remove(exstingSeries);
			await _dbContext.SaveChangesAsync();
			return exstingSeries;
		}
		public async Task<List<Series>> GetAllAsync()
		{
			return await _dbContext.Series.ToListAsync();
		}
		public async Task<Series?> GetByIdAsync(int id)
		{
			return await _dbContext.Series.FirstOrDefaultAsync(s => s.Id == id);
		}
		public async Task<Series> UpdateAsync(int id, Series series)
		{
			var existingSeries = await _dbContext.Series.FirstOrDefaultAsync(x => x.Id == id);
			if (existingSeries == null)
			{
				return null;
			}

			existingSeries.Name = series.Name;
			existingSeries.Description = series.Description;

			await _dbContext.SaveChangesAsync();
			return existingSeries;
		}
		#endregion
	}
}
