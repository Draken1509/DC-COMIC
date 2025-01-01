using HUUTRUNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HUUTRUNG.DataAccess.Repository.IRepository
{
    public interface IComicRepository : IRepository<Comic>
    {
		
		void Update(Comic obj);
		//void Save();
		PagedList<Comic> GetComics(
				string? search = null,
				string? filterType = null,
				string? filterWriter = null,
				string? sortBy = null,
				int pageNumber = 1,
				int pageSize = 10
			);

	}
}
