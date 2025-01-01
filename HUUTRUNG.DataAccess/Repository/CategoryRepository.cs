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
{ // Thắc mắc lớn:vừa kế thừa,vừa triển IC.
  // không cần định nghĩa lại toàn bộ IC trước đó mà vẫn không lỗi
  // À hiểu :)))))
    public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;	
		public CategoryRepository(ApplicationDbContext db):base(db) {
			_db = db;			
		}
		//public void Save()
		//{
		//	_db.SaveChanges();
		//}
		public void Update(Category obj)
		{
			//_db.Categories.Update(obj);
		}
	}
}
