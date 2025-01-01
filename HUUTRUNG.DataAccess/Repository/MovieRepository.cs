using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HUUTRUNG.DataAccess.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
	{
		private readonly ApplicationDbContext _db;	
		public MovieRepository(ApplicationDbContext db):base(db) {
			_db = db;			
		}		
		public void Update(Movie obj){           
             _db.SaveChanges();           
        }
		public PagedList<Movie> GetMovie(
			 string? search = null,
			 string? filterType = null,
			 string? filterGenre = null,
			 string? sortBy = null,
			 int pageNumber = 1,
			 int pageSize = 8
		 )
		{
			// Điều kiện lọc
			Expression<Func<Movie, bool>> filter = c =>
				(string.IsNullOrEmpty(search) || EF.Functions.Like(c.Name, $"%{search}%")) &&
				(string.IsNullOrEmpty(filterType) || EF.Functions.Like(c.MovieCategory.Name, $"%{filterType}%")) &&
				(string.IsNullOrEmpty(filterGenre) || c.Genres.Any(g => EF.Functions.Like(g.Name, $"%{filterGenre}%")));


			// Sắp xếp
			Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderBy = sortBy?.ToLower() switch
			{
				"name" => q => q.OrderBy(c => c.Name),
				"producer" => q => q.OrderBy(c => c.Producer),
				"writer" => q => q.OrderBy(c => c.Writer),
				_ => q => q.OrderBy(c => c.Id) // Mặc định sắp xếp theo Id
			};

			// Gọi phương thức dùng chung
			return GetFilteredAndPaged(
				filter: filter,
				orderBy: orderBy,
				pageNumber: pageNumber,
				pageSize: pageSize,
				includeProperties: "Genres"
			);
		}

	}
}
