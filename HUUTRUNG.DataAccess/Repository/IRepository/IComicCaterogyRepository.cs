using HUUTRUNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUUTRUNG.DataAccess.Repository.IRepository
{
    public interface IComicCaterogyRepository : IRepository<ComicCategory>
    {
        void Update(ComicCategory obj);
        //void Save();
    }
}
