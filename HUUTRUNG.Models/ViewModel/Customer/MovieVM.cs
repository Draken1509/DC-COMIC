using HUUTRUNG.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HUUTRUNG.Models.ViewModel.Customer
{
    public class MovieVM
	{
		public List<Movie> MovieList {  get; set; }	
		public PagedList<Movie> MovieListPagination { get; set; }
		public List<Movie> Take10MovieList { get; set; } //TRAILERS, CLIPS AND MORE
		public List<News> Take10MovieNewsList { get; set; } // ALL THE LATEST MOVIE NEWS
		public List<Movie> Take10MovieBelongAdventuresList { get; set; } //	ANIMATED ADVENTURES
		public List<Movie> Take10MovieBelongBigHeros { get; set; }  //BIG SCREEN, BIGGER HEROES
		public List<Movie> OnMax { get; set; }
		public Movie MovieMain { get; set; }	

		public List<String> GreneList { get; set; }

		public List<MovieCategory> TypeMovieList { get; set; }
		
	}
}
