using HUUTRUNG.DataAccess.Data;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HUUTRUNG.DataAccess.Repository
{
    public class ComicRepository : Repository<Comic>, IComicRepository
	{
		private readonly ApplicationDbContext _db;	
		public ComicRepository(ApplicationDbContext db):base(db) {
			_db = db;			
		}
		//public void Save()
		//{
		//	_db.SaveChanges();
		//}
		public void Update(Comic obj)
		{
            var objFromDb = _db.Comics.FirstOrDefault(c => c.Id == obj.Id);
            if (objFromDb != null)
            {
                // Cập nhật từng thuộc tính
                objFromDb.Name = obj.Name;
                objFromDb.Writer = obj.Writer;
                objFromDb.ArtBy = obj.ArtBy;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Colorist = obj.Colorist;
                objFromDb.Cover = obj.Cover;
                objFromDb.OnSaleDate = obj.OnSaleDate;
                objFromDb.PageCount = obj.PageCount;
                objFromDb.Rated = obj.Rated;              
                objFromDb.ComicCategoryId = obj.ComicCategoryId;
                objFromDb.SeriesId = obj.SeriesId;
                objFromDb.Description = obj.Description;
                objFromDb.IsFree = obj.IsFree;
                objFromDb.IsNew = obj.IsNew;

                if (obj.Thumbnail !=null)
                {
                    objFromDb.Thumbnail = obj.Thumbnail;
                }            
                // Lưu thay đổi vào database
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("Comic not found");
            }
        }


		public PagedList<Comic> GetComics(
			string search = null,
			string filterType = null,
			string filterWriter = null,
			string sortBy = null,
			int pageNumber = 1,
			int pageSize = 10
		)
		{
			// Điều kiện lọc
			Expression<Func<Comic, bool>> filter = c =>
				(string.IsNullOrEmpty(search) || EF.Functions.Like(c.Name, $"%{search}%")) &&
				(string.IsNullOrEmpty(filterType) || EF.Functions.Like(c.ComicCategory.Name, $"%{filterType}%")) &&
				(string.IsNullOrEmpty(filterWriter) || EF.Functions.Like(c.Writer, $"%{filterWriter}%"));

			// Sắp xếp
			Func<IQueryable<Comic>, IOrderedQueryable<Comic>> orderBy = sortBy?.ToLower() switch
			{
				"name" => q => q.OrderBy(c => c.Name),
				"price" => q => q.OrderBy(c => c.Price),
				"writer" => q => q.OrderBy(c => c.Writer),
				_ => q => q.OrderBy(c => c.Id) // Mặc định sắp xếp theo Id
			};

			// Gọi phương thức dùng chung
			return GetFilteredAndPaged(
				filter: filter,
				orderBy: orderBy,
				pageNumber: pageNumber,
				pageSize: pageSize,
				includeProperties: "ComicCategory"
            );
		}


	}
}
