using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.DataAccess.Repository
{
    public class NewsCategoryRepository : Repository<NewsCategory>, INewsCategoryRepository
	{
		private readonly ApplicationDbContext _db;	
		public NewsCategoryRepository(ApplicationDbContext db):base(db) {
			_db = db;			
		}	
		public void Update(NewsCategory obj)
		{
            _db.NewsCategories.Update(obj);
        }
	}
}
