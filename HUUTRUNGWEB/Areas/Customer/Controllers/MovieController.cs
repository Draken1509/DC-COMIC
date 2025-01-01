using HUUTRUNG.DataAccess.Repository;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models;
using HUUTRUNG.Models.ViewModel.Admin;
using HUUTRUNG.Models.ViewModel.Customer;
using HUUTRUNG.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using X.PagedList;
using System.Drawing.Printing;
using System.Globalization;
using System.Security.Claims;
using X.PagedList.Extensions;
using System;
using HUUTRUNG.Models.Domain;


namespace HUUTRUNGWEB.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class MovieController : Controller
    {
		private readonly ILogger<MovieController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public MovieController(ILogger<MovieController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
     		
		public IActionResult Index(
			string? search = null, 
			string? sortBy = null,     
			string? filterType = null, 
			string? filterGenre = null, 
			int? page = 1)  
			{
			int pageSize = 8;
			int pageNumber = page ?? 1;

			var pagedMovie= _unitOfWork.Movie.GetMovie(
				search: search,
				filterType: filterType,
				filterGenre: filterGenre,
				sortBy: sortBy,
				pageNumber: pageNumber,
				pageSize: pageSize);

			var movieList = _unitOfWork.Movie.GetAll().ToList();  // Từ DB
			var movieListIntial = movieList;
			var take10MovieNewsList = _unitOfWork.News.GetAll().OrderByDescending(n => n.PublishDate).Select(n => new News
			{
				Id = n.Id,
				Thumbnail = n.Thumbnail,
				Tittle = n.Tittle,				
			}).Take(10).ToList();

			var take10MovieList =  _unitOfWork.Movie.GetAll().OrderByDescending(m => m.PublishDate)
				.Select(n => new Movie
				{
					Id = n.Id,
					UrlVideo = n.UrlVideo,
					Title = n.Title,
					Name=n.Name,
					
				})
				.Take(10).ToList();

			var take10MovieBelongAdventuresList = _unitOfWork.Movie
				.GetAll(u => u.Genres.Any(g => g.Name == "Adventure"), includeProperties: "Genres")
				.Select(n => new Movie
				{
					Id = n.Id,
					Thumbnail = n.Thumbnail,
					Name = n.Name
				})
				.Take(10).ToList();

			var take10MovieBelongBigHeros = _unitOfWork.Movie
				.GetAll(u => u.Genres.Any(g => g.Name == "BigHeros"), includeProperties: "Genres").Take(10).ToList();

			var movieMain = _unitOfWork.Movie.Get(u => u.IsMain == true);

			var onMax = _unitOfWork.Movie
				.GetAll(u => u.MovieCategory.Name == "DC ON MAX").OrderByDescending(n => n.PublishDate)
				.Select(n => new Movie
				{
					Id = n.Id,
					Thumbnail = n.Thumbnail,
					Title = n.Name
				})
				.Take(10).ToList();

			MovieVM MovieVM = new()
			{
				//MovieList = movieListIntial,
				MovieListPagination= pagedMovie,
				Take10MovieNewsList = take10MovieNewsList,
				Take10MovieList = take10MovieList,  // 10 movie moi nhat
				Take10MovieBelongAdventuresList = take10MovieBelongAdventuresList,
				Take10MovieBelongBigHeros =take10MovieBelongBigHeros,
				MovieMain = movieMain,
				OnMax=onMax,
				TypeMovieList = _unitOfWork.MovieCategory.GetAll().ToList(),
				GreneList = _unitOfWork.Genre.GetAll(c => c.Name != null)
									.Select(c => c.Name)
									.Distinct()
									.ToList(),
			};

			return View(MovieVM);
		}

		public IActionResult NotAvailable()
		{
			return View();
		}

		public IActionResult Details(int? MovieId)
		{
			Movie Movie = _unitOfWork.Movie.Get(u => u.Id == MovieId, includeProperties: "MovieCategory");			
			return View(Movie);
		}
	}


}

