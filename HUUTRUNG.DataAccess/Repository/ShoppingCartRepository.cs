using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
	{
		private readonly ApplicationDbContext _db;	
		public ShoppingCartRepository(ApplicationDbContext db):base(db) {
			_db = db;			
		}	
		public void Update(ShoppingCart obj)
		{
			_db.ShoppingCarts.Update(obj);

		}
	}
}
