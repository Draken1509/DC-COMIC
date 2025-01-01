using AutoMapper;
using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;
using HUUTRUNG.Utility;

using HUUTRUNG_WEBAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUUTRUNG.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IApplicationUserAPIRepository _commentRepository;
        private readonly IMapper _mapper;
        public ApplicationUserController(ApplicationDbContext dbContext, IApplicationUserAPIRepository commentRepository,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = SD.Role_Customer)]
        [HttpPost("create-comment")]
        public async Task<IActionResult> createComment(CommentRequestDTO? commentRequestDTO)
        {         
            var commentDomain = await _commentRepository.CreateComment(commentRequestDTO);
            if (commentDomain == null)
            {
                return NotFound();
            }
            var commentDTO = _mapper.Map<CommentResponseDTO>(commentDomain);
            return Ok(commentDTO);
        }
		
       
		[Authorize(SD.Role_Customer)]
		[HttpPost("rate")]
		public async Task<IActionResult> RateComic(RatingRequestDTO requestDTO)
		{

			var RatingDomain = await _commentRepository.RateComic(requestDTO);

			if (RatingDomain == null)
			{
				return NotFound();
			}

			var RatingDTO = _mapper.Map<RatingRequestDTO>(RatingDomain);
			return Ok(RatingDTO);
		}
	}
}
