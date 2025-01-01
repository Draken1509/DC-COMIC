using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;
using Microsoft.AspNetCore.Mvc;

namespace HUUTRUNG_WEBAPI.Repositories
{
    public interface IComicAPIRepository
    {
       public Task<ComicPagedDTO> GetAllAsync(string? filterOn=null, string? filterQuery=null,
             [FromQuery] string? sortBy=null, [FromQuery] bool? isAscending = null,
             [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000);

       public Task<List<ComicDTO?>> GetLastestComicAsync();
       public Task<List<ComicDTO?>> GetHighestRatingComicAsync();
       public Task<ComicDTO?> GetByIdAsync(int id);      
       public Task<List<PageDTO>> GetPagebyIdAsync(int comicId);    
    

		#region không sử dụng
		//public Task<Comic> CreateAsync(Comic Comic);
		//public Task<Comic> UpdateAsync(int id,Comic Comic);
		//public Task<Comic?> DeleteAsync(int id);
		#endregion

	}

}
