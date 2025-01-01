using AutoMapper;
using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;
using HUUTRUNG.Utility;
using HUUTRUNG_WEBAPI.CustomActionFilters;
using HUUTRUNG_WEBAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUUTRUNG_WEBAPI.Controllers
{

	#region không sử dụng
	//[HttpDelete]
	//[Authorize(Roles = "Writer")]
	//[Route("{id:int}")]
	//public async Task<IActionResult> Delete([FromRoute] int id)
	//{
	//	//var Comic = _dbContext.Comic.Find(id);
	//	var ComicDomain = await _ComicRepository.DeleteAsync(id);

	//	if (ComicDomain == null)
	//	{
	//		return NotFound();
	//	}
	//	var ComicDTO = _mapper.Map<ComicDTO>(ComicDomain);
	//	return Ok(ComicDTO);
	//}

	//[Route("{id:int}")]
	//public async Task<IActionResult> Update([FromRoute] int id, UpdateComicRequestDTO updateComicRequestDTO)
	//{
	//	var ComicDomain = _mapper.Map<Comic>(updateComicRequestDTO);

	//	ComicDomain = await _ComicRepository.UpdateAsync(id, ComicDomain);

	//	if (ComicDomain == null)
	//	{
	//		return NotFound();
	//	}

	//	var ComicDTO = _mapper.Map<ComicDTO>(ComicDomain);
	//	return Ok(ComicDTO);
	//}
	////[ValidateModel]
	//// [Authorize(Roles = "Writer")]
	//[HttpPost]
	//public async Task<IActionResult> Create([FromBody] AddComicRequestDTO addComicRequestDTO)
	//{

	//	//Map or covert DTO to Domain
	//	var ComicDomain = _mapper.Map<Comic>(addComicRequestDTO);

	//	//use Domain Model to create Comic
	//	ComicDomain = await _ComicRepository.CreateAsync(ComicDomain);

	//	//Map Domain model back to DTO
	//	var ComicDTO = _mapper.Map<ComicDTO>(ComicDomain);
	//	return Ok(ComicDTO);

	//}
	// E46D4C92-2F7B-4BC6-A746-461F96C6544B
	// F3041C35-7C48-4A2D-81E4-7F881E715DB3


	#endregion

	[Route("api/[controller]")]
    [ApiController]  
    public class ComicController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IComicAPIRepository _ComicRepository;
        private readonly IMapper _mapper;
        public ComicController(ApplicationDbContext dbContext, IComicAPIRepository ComicRepository,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _ComicRepository = ComicRepository; 
            _mapper = mapper;
        }

        // GET -https://localhost:????/api/comic
        // GET: /api/comic?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]   
		//[Authorize(Roles = SD.Role_Employee)]
		public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var listComicDTO = await _ComicRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending ?? true,pageNumber,pageSize);// isAscending neu null la true
            return Ok(listComicDTO);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLastestComic()  // 12 comic phát hành mới nhất
        {
            var listComicDTO = await _ComicRepository.GetLastestComicAsync();
            return Ok(listComicDTO);
        }

        [HttpGet("highest-rating")]
        public async Task<IActionResult> GetHighestRatingComic()    // 10 comic có điểm cao nhất
        {
            var listComicDTO = await _ComicRepository.GetHighestRatingComicAsync();
            return Ok(listComicDTO);
        }
      
        [HttpGet]
        [Route("{id:int}")]      
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //var Comic = _dbContext.Comic.Find(id);
            var ComicDTO = await _ComicRepository.GetByIdAsync(id);
            if (ComicDTO == null)
            {
                return NotFound();
            }
            return Ok(ComicDTO);
        }

        [HttpGet("page/{id:int}")]        
        public async Task<IActionResult> GetPageById([FromRoute] int id)
        {
            //var Comic = _dbContext.Comic.Find(id);
            var PageDTO = await _ComicRepository.GetPagebyIdAsync(id);
            if (PageDTO == null)
            {
                return NotFound();
            }
            return Ok(PageDTO);
        }
                  
    }
   
}
