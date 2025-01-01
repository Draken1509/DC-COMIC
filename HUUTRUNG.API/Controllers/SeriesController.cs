using AutoMapper;
using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;

using HUUTRUNG_WEBAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HUUTRUNG_WEBAPI.Controllers
{


    //[Làm cho biết restful API chứ không có ý nghĩa trong project ]

    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISeriesAPIRepository _seriesRepository;
        private readonly IMapper _mapper;
        public SeriesController(ApplicationDbContext dbContext, ISeriesAPIRepository seriesRepository,
            IMapper mapper)
        {
            this._dbContext = dbContext;
            this._seriesRepository = seriesRepository;   
            this._mapper = mapper;
        }

        // GET -https://localhost:????/api/comic
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var seriesDomain = await _seriesRepository.GetAllAsync();                         
            return Ok(_mapper.Map<List<SeriesDTO>>(seriesDomain));
        }
      
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //var series = _dbContext.Series.Find(id);
            var seriesDomain =  await _seriesRepository.GetByIdAsync(id);
            if (seriesDomain == null)
            {
                return NotFound();
            }      
            return Ok(_mapper.Map<SeriesDTO>(seriesDomain));
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSeriesRequestDTO addSeriesRequestDTO )
        {

            //Map or covert DTO to Domain
            var seriesDomain =_mapper.Map<Series>(addSeriesRequestDTO);

            //use Domain Model to create Series
            seriesDomain =  await _seriesRepository.CreateAsync(seriesDomain);
             
            //Map Domain model back to DTO
            var seriesDTO = _mapper.Map<SeriesDTO>(seriesDomain);
             
            return CreatedAtAction(nameof(GetById), new {id=seriesDTO.Id}, seriesDTO);
        }


        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateSeriesRequestDTO updateSeriesRequestDTO)
        {
            var seriesDomain = _mapper.Map<Series>(updateSeriesRequestDTO);

            seriesDomain = await _seriesRepository.UpdateAsync(id, seriesDomain);
           
            if(seriesDomain == null)
            {
                return NotFound();  
            }

            var seriesDTO = _mapper.Map<SeriesDTO>(seriesDomain);
            return Ok(seriesDTO);            
        }


        [HttpDelete]
        [Route("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //var series = _dbContext.Series.Find(id);
            var seriesDomain = await _seriesRepository.DeleteAsync(id);

            if (seriesDomain == null){
                return NotFound();
            }
            var seriesDTO = _mapper.Map<SeriesDTO>(seriesDomain);
            return Ok(seriesDTO);
        }

    }
}
