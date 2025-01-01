using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Threading;

namespace HUUTRUNG_WEBAPI.Repositories
{
    public class SQLComicRepository : IComicAPIRepository
    {
       
        private readonly ApplicationDbContext _dbContext; 
        public SQLComicRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;      
        }       
        public async Task<ComicPagedDTO> GetAllAsync(string? filterOn = null, string? filterQuery = null,
           [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = null,
           [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {

          

            var comics = _dbContext.Comics
                .Include(x => x.Ratings)
                .Include(c => c.ComicCategory)
                .Where(comic => comic.IsFree)
                .Select(comic => new ComicDTO
                {
                    Id = comic.Id,
                    Name = comic.Name,
                    Thumbnail = comic.Thumbnail,
                    AverageRating = comic.AverageRating,
                    ComicCategory = comic.ComicCategory,
                    ComicCategoryId = (int)comic.ComicCategoryId,
                    OnSaleDate = comic.OnSaleDate
                })
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                switch (filterOn.ToLower())
                {
                    case "name":
                        comics = comics.Where(x => x.Name.Contains(filterQuery));
                        break;
                    // Add more filters if needed
                    default:
                        break;
                }
            }

            // Sorting 
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    comics = isAscending == true ? comics.OrderBy(x => x.Name) : comics.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Writer", StringComparison.OrdinalIgnoreCase))
                {
                    comics = isAscending == true ? comics.OrderBy(x => x.Writer) : comics.OrderByDescending(x => x.Writer);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            var totalItems = await comics.CountAsync();
            var pagedComics = await comics.Skip(skipResults).Take(pageSize).ToListAsync();

            // Return paged results along with total items count
            return new ComicPagedDTO
            {
                Comics = pagedComics,
                TotalItems = totalItems,               
            };

        }
        public async Task<List<ComicDTO>> GetLastestComicAsync()
        {
            // Tính toán lại AverageRating nếu có ratings
          

            return await _dbContext.Comics
                .AsNoTracking() // Không cần theo dõi thay đổi
                .Where(comic => comic.IsFree)
                .OrderByDescending(comic => comic.OnSaleDate) // Sắp xếp theo ngày phát hành
                .Select(comic => new ComicDTO
                {
                    Id = comic.Id,
                    Name = comic.Name,
                    Thumbnail = comic.Thumbnail,
                    AverageRating = comic.Ratings.Any()? Math.Round((double)comic.Ratings.Average(r => r.Score), 1): 0,

                    ComicCategory = comic.ComicCategory, // Nếu cần hiển thị thông tin danh mục
                    ComicCategoryId = comic.ComicCategoryId ?? 0, // Xử lý nếu null
                    OnSaleDate = comic.OnSaleDate
                })
                .Take(12) // Lấy 12 kết quả đầu tiên
                .ToListAsync();
        }
        public async Task<List<ComicDTO>> GetHighestRatingComicAsync()
        {
            var comics =  _dbContext.Comics                                        
                                         .Include(c => c.Ratings)
                                         .Include(c => c.ComicCategory)
                                         .Where(comic => comic.IsFree)
                                         .AsQueryable();

            return await  comics.OrderByDescending(comic => comic.Ratings.Any()
                        ? comic.Ratings.Average(r => r.Score)
                        : 0)
                    .Take(10)
                    .Select(comic => new ComicDTO
                    {
                        Id = comic.Id,
                        Name = comic.Name,
                        Thumbnail = comic.Thumbnail,
                        ComicCategory = comic.ComicCategory,
                        ComicCategoryId = (int)comic.ComicCategoryId,
                        OnSaleDate = comic.OnSaleDate,
                        AverageRating = comic.Ratings.Any() ? Math.Round((double)comic.Ratings.Average(r => r.Score), 1) : 0,
                    })
                    .ToListAsync();
        }     
        public async Task<ComicDTO?> GetByIdAsync(int id)
        {
            var comic = await _dbContext.Comics
                .Include(x => x.Series)
                .Include(x => x.ComicCategory)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (comic == null) return null;

            // Lấy danh sách các trang của comic
            var pages = await _dbContext.Pages
                .Where(p => p.ComicId == id)
                .Select(p => new PageDTO
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    PageNumber = p.PageNumber
                }).ToListAsync();

            // Lấy tất cả các comment (không phân biệt cấp độ)
            var allComments = await _dbContext.Comments
                .Where(c => c.ComicId == id)
                .Include(c => c.Replies)                
                .Select(c => new CommentDTO
                {
                    CommentId = c.CommentId,
                    Content = c.Content,
                    LikeCount = c.LikeCount,
                    CreateAt = c.CreateAt,
                    ApplicationUser = new ApplicationUserDTO
                    {
                        Id = c.ApplicationUser.Id,
                        UserName = c.ApplicationUser.UserName
                    },

                    ParentCommentId = c.ParentCommentId,
                    ReplyUserName = c.ParentComment.ApplicationUser.UserName,
                    Replies = new List<CommentDTO>()
                })
                .ToListAsync();

            // Build the replies tree
            var comments = BuildCommentHierarchy(allComments);

            // Lấy danh sách các rating
            var ratings = await _dbContext.Ratings
                .Where(r => r.ComicId == id)
                .Select(r => new RatingDTO
                {
                    Id = r.Id,
                    Score = r.Score
                }).ToListAsync();

            // Tính toán lại AverageRating nếu có ratings
            var averageRating = ratings.Any()
                      ? Math.Round((decimal)ratings.Average(r => r.Score), 1) // Làm tròn về 1 chữ số thập phân
                      : 0m; // Đảm bảo sử dụng 0m cho kiểu decimal



            // Lấy danh sách các comic liên quan
            var relatedComics = await _dbContext.Comics
                .Where(c => c.SeriesId == comic.SeriesId && c.Id != id)
                .Select(c => new ComicDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();

            // Trả về ComicDTO
            return new ComicDTO
            {

                Id = comic.Id,
                Name = comic.Name,
                Description = comic.Description,
                Thumbnail = comic.Thumbnail,
                Series = comic.Series,
                ComicCategory = comic.ComicCategory,
                AverageRating = (double)averageRating,
                Pages = pages,
                Comments = comments,
                Ratings = ratings,
                RelatedComics = relatedComics
            };
        }        
        private List<CommentDTO> BuildCommentHierarchy(List<CommentDTO> allComments)
        {
            var commentDict = allComments.ToDictionary(c => c.CommentId);
            var rootComments = new List<CommentDTO>();

            foreach (var comment in allComments)
            {
                if (comment.ParentCommentId == null || comment.ParentCommentId == comment.CommentId ) // Bình luận gốc
                {
                    rootComments.Add(comment);
                }
                else
                {
                    // Tìm bình luận cha và thêm vào danh sách replies
                    if (commentDict.ContainsKey(comment.ParentCommentId.Value))
                    {
                        var parentComment = commentDict[comment.ParentCommentId.Value];
                        parentComment.Replies.Add(comment);
                    }
                }
            }

            return rootComments;
        }     
        public async Task<List<PageDTO>> GetPagebyIdAsync(int comicId)
        {
            return await _dbContext.Pages
                .Where(p => p.ComicId == comicId)
                .Select(p => new PageDTO 
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    PageNumber = p.PageNumber
                })
                .ToListAsync();
        }     
    

		#region Không sử dụng
		public async Task<Comic> UpdateAsync(int id, Comic Comic)
		{
			var existingComic = await _dbContext.Comics.Include(x => x.Series).Include(x => x.ComicCategory).FirstOrDefaultAsync(x => x.Id == id);
			if (existingComic == null)
			{
				return null;
			}

			existingComic.Name = Comic.Name;
			existingComic.Description = Comic.Description;
			existingComic.Writer = Comic.Writer;
			existingComic.Thumbnail = Comic.Thumbnail;
			existingComic.ArtBy = Comic.ArtBy;
			existingComic.Cover = Comic.Cover;
			existingComic.Colorist = Comic.Colorist;
			existingComic.ComicCategoryId = Comic.ComicCategoryId;
			existingComic.SeriesId = Comic.SeriesId;

			await _dbContext.SaveChangesAsync();
			return existingComic;
		}
		public async Task<Comic> CreateAsync(Comic comic)
		{
			await _dbContext.Comics.AddAsync(comic);
			await _dbContext.SaveChangesAsync();
			return comic;
		}
		public async Task<Comic> DeleteAsync(int id)
		{
			var exstingComic = await _dbContext.Comics.FirstOrDefaultAsync(s => s.Id == id);
			if (exstingComic == null)
			{
				return null;
			}
			_dbContext.Comics.Remove(exstingComic);
			await _dbContext.SaveChangesAsync();
			return exstingComic;
		}
		#endregion

	}
}
