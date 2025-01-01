using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.DataAccess.Repository
{
	// Bất lợi của UnitOfWork:
	// nếu chỉ muốn sử dụng CategoryRepository nhưng UOW nó sẽ tạo ra triển khai tất cả các Repo đã đăng kí

	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		public ICategoryRepository Category{ get; private set; }
        public ICharacterRepository Character{ get; private set; }
        public IComicRepository Comic { get; private set; }
        public IComicCaterogyRepository ComicCategory { get; private set; }
        public ISeriesRepository Series { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
		public IOrderDetailRepository OrderDetail { get; private set; }
		public IOrderHeaderRepository OrderHeader { get; private set; }
		public INewsRepository News { get; private set; }
        public INewsCategoryRepository NewsCategory { get; private set; }
		public IMovieRepository Movie { get; private set; }
		public IMovieCategoryRepository MovieCategory { get; private set; }
		public IGenreRepository Genre { get; private set; }


		public UnitOfWork (ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);  
			Character =new CharacterRepository(_db);
            Comic= new ComicRepository(_db);
			ComicCategory = new ComicCategoryRepository(_db);
            Series = new SeriesRepository(_db);
			Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
		    ApplicationUser = new ApplicationUserRepository(_db);
			OrderDetail = new OrderDetailRepository(_db);
			OrderHeader = new OrderHeaderRepository(_db);
			News = new NewsRepository(_db);
			NewsCategory = new NewsCategoryRepository(_db);
			Movie = new MovieRepository(_db);
			MovieCategory = new MovieCategoryRepository(_db);
			Genre = new GenreRepository(_db);
		}
		
		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
