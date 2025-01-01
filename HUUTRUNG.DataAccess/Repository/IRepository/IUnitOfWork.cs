using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository Category{ get; }
        ICharacterRepository Character { get; }
		IComicRepository Comic { get; }
        IComicCaterogyRepository ComicCategory { get; }
        ISeriesRepository Series { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
		IOrderDetailRepository OrderDetail { get; }
		IOrderHeaderRepository OrderHeader { get; }
		INewsRepository News { get; }
        INewsCategoryRepository NewsCategory{ get; }

		IMovieRepository Movie { get; }
		IMovieCategoryRepository MovieCategory { get; }
		IGenreRepository Genre { get; }
		void Save();

	}
}
