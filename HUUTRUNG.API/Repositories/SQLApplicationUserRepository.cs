using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;
using HUUTRUNG.Models.ViewModel.Admin;

using Microsoft.EntityFrameworkCore;

namespace HUUTRUNG_WEBAPI.Repositories
{
    public class SQLApplicationUserRepository : IApplicationUserAPIRepository
    {
        private readonly ApplicationDbContext _dbContext;        
        public SQLApplicationUserRepository(ApplicationDbContext dbContext) {         
            this._dbContext = dbContext;   
        }
        public async Task<Comment> CreateComment(CommentRequestDTO? CommentDTO)
        {
            if (CommentDTO.ApplicationUserId == null || CommentDTO.ComicId == null)
            {
                return null;
            }              
            var comment = new Comment
            {
                ComicId = CommentDTO.ComicId,
                ApplicationUserId = CommentDTO.ApplicationUserId,				
                CreateAt = DateTime.UtcNow,
                Content = CommentDTO.Content,
                ParentCommentId = CommentDTO.ParentCommentId

            };
            if (CommentDTO.ParentCommentId != null)
            {
                var parentComment = await _dbContext.Comments
                                                    .Include(c => c.ApplicationUser) // Nạp ApplicationUser cùng với Comment
                                                    .Where(c => c.CommentId == CommentDTO.ParentCommentId)
                                                    .FirstOrDefaultAsync();

                if (parentComment != null && parentComment.ApplicationUser != null)
                {
                    comment.ReplyUserName = parentComment.ApplicationUser.Email;
                }
            }
             _dbContext.Comments.AddAsync(comment);
             await _dbContext.SaveChangesAsync();
             return comment;
        }
		public async Task<Rating> RateComic(RatingRequestDTO requestDTO)
		{
			if (requestDTO.Score < 1 || requestDTO.Score > 5)
			{
				throw new ArgumentException("Invalid star value. It must be between 1 and 5.");
			}

			// Kiểm tra xem người dùng đã đánh giá comic này chưa
			var existingRating = await _dbContext.Ratings
				.FirstOrDefaultAsync(r => r.ComicId == requestDTO.ComicId && r.ApplicationUserId == requestDTO.ApplicationUserId);

			if (existingRating != null)
			{
				// Nếu đã có đánh giá, cập nhật điểm
				existingRating.Score = requestDTO.Score;
				await _dbContext.SaveChangesAsync();
				return existingRating;
			}
			else
			{
				// Nếu chưa có đánh giá, tạo mới
				var newRating = new Rating
				{
					ComicId = requestDTO.ComicId,
					ApplicationUserId = requestDTO.ApplicationUserId,
					Score = requestDTO.Score
				};

				_dbContext.Ratings.Add(newRating);
				await _dbContext.SaveChangesAsync();
				return newRating;
			}
		}



	}
}
