using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;
using HUUTRUNG.Models.ViewModel.Admin;

using Microsoft.EntityFrameworkCore;

namespace HUUTRUNG_WEBAPI.Repositories
{
    public class SQLBookmarkRepository : IBookmarkAPIRepository
    {
        private readonly ApplicationDbContext _dbContext;        
        public SQLBookmarkRepository(ApplicationDbContext dbContext) {         
            this._dbContext = dbContext;   
        }
        public async Task<Bookmark> AddOrUpdateBookmarkAsync(string userId, int ComicId, BookmarkRequestDTO bookmarkDTO)
        {
            // Tìm bookmark dựa trên ComicId và userId
            var existingBookmark = await _dbContext.Bookmarks
                .FirstOrDefaultAsync(b => b.ComicId == ComicId && b.ApplicationUserId == userId);

            if (existingBookmark == null)
            {
                // Nếu chưa tồn tại, thêm mới
                var newBookmark = new Bookmark
                {
                    ComicId = ComicId,
                    ApplicationUserId = userId,
                    IsRead = bookmarkDTO.IsRead,
                    IsSave = bookmarkDTO.IsSave,
                    IsCurrentlyReading = bookmarkDTO.IsCurrentlyReading,
                  //  SavedAt = DateTime.UtcNow,
                };
                _dbContext.Bookmarks.Add(newBookmark);
                await _dbContext.SaveChangesAsync();
                return newBookmark;
            }
            else
            {
                // Nếu đã tồn tại, cập nhật
                existingBookmark.ComicId =ComicId;
                existingBookmark.IsRead = bookmarkDTO.IsRead;
                existingBookmark.IsSave = bookmarkDTO.IsSave;
                existingBookmark.IsCurrentlyReading = bookmarkDTO.IsCurrentlyReading;
           //   existingBookmark.SavedAt = DateTime.UtcNow;
                _dbContext.Bookmarks.Update(existingBookmark);
                await _dbContext.SaveChangesAsync();
                return existingBookmark;
            }
        }
        public async Task<BookmarkResponseDTO> GetStatusBookmark(string? userId, int? comicId)
        {
            if (userId == null || comicId == null)  
            {
                return new BookmarkResponseDTO(false, false, false);
            }
            else
            {
                var status = await _dbContext.Bookmarks
               .Where(c => c.ApplicationUserId == userId && c.ComicId == comicId)
               .Select(c => new BookmarkResponseDTO
               {
                   IsSave = c.IsSave,
                   IsCurrentlyReading = c.IsCurrentlyReading,
                   IsRead = c.IsRead
               }).FirstOrDefaultAsync();

                return status;
            }
          
        }
		public async Task<List<ComicDTO>> GetSavedComicAsync(string id)
		{
			return await _dbContext.Bookmarks
				.AsNoTracking()
				.Where(b => b.ApplicationUserId == id && b.IsSave == true) // Lọc theo UserId
				.Select(b => new ComicDTO
				{
					Id = b.Comic.Id, // Liên kết tới bảng Comics qua Bookmark
					Name = b.Comic.Name,
					Thumbnail = b.Comic.Thumbnail,
					AverageRating = b.Comic.AverageRating,
					ComicCategory = b.Comic.ComicCategory,
					ComicCategoryId = b.Comic.ComicCategoryId ?? 0,
					OnSaleDate = b.Comic.OnSaleDate
				})
				.ToListAsync();
		}
		public async Task<List<ComicDTO>> GetReadComicAsync(string id)
		{
			return await _dbContext.Bookmarks
			   .AsNoTracking()
			   .Where(b => b.ApplicationUserId == id && b.Comic != null && b.IsRead == true)
			   .Select(b => new ComicDTO
			   {
				   Id = b.Comic!.Id,
				   Name = b.Comic.Name,
				   Thumbnail = b.Comic.Thumbnail,
				   AverageRating = b.Comic.AverageRating,
				   ComicCategory = b.Comic.ComicCategory,
				   ComicCategoryId = b.Comic.ComicCategoryId ?? 0,
				   OnSaleDate = b.Comic.OnSaleDate
			   })
			   .ToListAsync();
		}
		public async Task<List<ComicDTO>> GetCurrentlReadingComicAsync(string id)
		{
			return await _dbContext.Bookmarks
				.AsNoTracking()
				.Where(b => b.ApplicationUserId == id && b.IsCurrentlyReading == true) // Lọc theo UserId
				.Select(b => new ComicDTO
				{
					Id = b.Comic.Id, // Liên kết tới bảng Comics qua Bookmark
					Name = b.Comic.Name,
					Thumbnail = b.Comic.Thumbnail,
					AverageRating = b.Comic.AverageRating,
					ComicCategory = b.Comic.ComicCategory,
					ComicCategoryId = b.Comic.ComicCategoryId ?? 0,
					OnSaleDate = b.Comic.OnSaleDate
				})
				.ToListAsync();
		}

	}
}
