using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;


namespace HUUTRUNG_WEBAPI.Repositories
{
    public interface IBookmarkAPIRepository
    {
        public Task<Bookmark> AddOrUpdateBookmarkAsync(string userId, int comicId, BookmarkRequestDTO bookmarkDTO);
        public Task<BookmarkResponseDTO> GetStatusBookmark(string? userId, int? comicId);
		public Task<List<ComicDTO?>> GetSavedComicAsync(string id);
		public Task<List<ComicDTO?>> GetReadComicAsync(string id);
		public Task<List<ComicDTO?>> GetCurrentlReadingComicAsync(string id);
	}
}
