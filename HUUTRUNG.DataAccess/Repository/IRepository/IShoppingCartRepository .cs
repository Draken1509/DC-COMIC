using HUUTRUNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);
        //void Save();
    }
}
