using AutoMapper;
using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Utility;

using HUUTRUNG_WEBAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUUTRUNG.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IBookmarkAPIRepository BookmarkRepository;
        private readonly IMapper _mapper;
        public BookmarkController(ApplicationDbContext dbContext, IBookmarkAPIRepository BookmarkRepository,
            IMapper mapper)
        {
            _dbContext = dbContext;
            this.BookmarkRepository = BookmarkRepository;
            _mapper = mapper;
        }
        [Authorize(Roles = SD.Role_Customer)]
        [HttpPost,HttpPut]
        public async Task<IActionResult> UpsertBookmark(string userId, int comicId, BookmarkRequestDTO BookmarkRequestDTO)
        {         
            var BookmarkDomain = await BookmarkRepository.AddOrUpdateBookmarkAsync(userId, comicId, BookmarkRequestDTO);

            if (BookmarkDomain == null)
            {
                return NotFound();
            }
            var bookmarkDTO = _mapper.Map<BookmarkRequestDTO>(BookmarkDomain);
            return Ok(bookmarkDTO);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatusBookmark(string? userId, int? comicId)
        {
            var bookmarkResponse = await BookmarkRepository.GetStatusBookmark(userId, comicId);
            if (bookmarkResponse == null)
            {
                return NotFound();
            }
            var bookmarkDTO = _mapper.Map<BookmarkResponseDTO>(bookmarkResponse);
            return Ok(bookmarkDTO);
        }

		[HttpGet("saved/{id}")]
		[Authorize(Roles = SD.Role_Customer)]
		public async Task<IActionResult> GetSavedComic([FromRoute] string id)
		{
			var listComicDTO = await BookmarkRepository.GetSavedComicAsync(id);
			return Ok(listComicDTO);
		}

		[HttpGet("read/{id}")]
		[Authorize(Roles = SD.Role_Customer)]
		public async Task<IActionResult> GetReadComic([FromRoute] string id)
		{
			var listComicDTO = await BookmarkRepository.GetReadComicAsync(id);
			return Ok(listComicDTO);
		}

		[HttpGet("currently-reading/{id}")]
		[Authorize(Roles = SD.Role_Customer)]
		public async Task<IActionResult> GetCurrentlReadingComic([FromRoute] string id)
		{
			var listComicDTO = await BookmarkRepository.GetCurrentlReadingComicAsync(id);
			return Ok(listComicDTO);
		}


	}
}
