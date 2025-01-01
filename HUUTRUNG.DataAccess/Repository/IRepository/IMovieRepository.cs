using HUUTRUNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HUUTRUNG.DataAccess.Repository.IRepository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie obj);
		PagedList<Movie> GetMovie(
				 string? search = null,
				 string? filterType = null,
				 string? filterGenre = null,
				 string? sortBy = null,
				 int pageNumber = 1,
				 int pageSize = 10
			 );
	}
}
