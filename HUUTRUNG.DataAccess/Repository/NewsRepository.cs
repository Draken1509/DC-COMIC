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
    public class NewsRepository : Repository<News>, INewsRepository
	{
		private readonly ApplicationDbContext _db;	
		public NewsRepository(ApplicationDbContext db):base(db) {
			_db = db;			
		}

        public void Update(News obj)
        {
            var objFromDb = _db.News.FirstOrDefault(c => c.Id == obj.Id);
            if (objFromDb != null)
            {
                // Cập nhật từng thuộc tính
                objFromDb.Tittle = obj.Tittle;
                objFromDb.Author = obj.Author;
                objFromDb.Content = obj.Content;
                if (objFromDb.PublishDate != null)
                {
                    objFromDb.PublishDate = obj.PublishDate;
                }            
             
                    objFromDb.NewsCategoryId = obj.NewsCategoryId;
                                    
                if (obj.Thumbnail != null){
                    objFromDb.Thumbnail = obj.Thumbnail;
                }
                // Lưu thay đổi vào database
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("News not found");
            }
        }
        //public void Save()
        //{
        //	_db.SaveChanges();
        //}
        //public void Update(News obj)
        //{
        //          var objFromDb = _db.Newss.FirstOrDefault(c => c.Id == obj.Id);
        //          if (objFromDb != null)
        //          {
        //              // Cập nhật từng thuộc tính
        //              objFromDb.Name = obj.Name;
        //              objFromDb.Writer = obj.Writer;
        //              objFromDb.ArtBy = obj.ArtBy;
        //              objFromDb.Price = obj.Price;
        //              objFromDb.Price50 = obj.Price50;
        //              objFromDb.Price100 = obj.Price100;
        //              objFromDb.Colorist = obj.Colorist;
        //              objFromDb.Cover = obj.Cover;
        //              objFromDb.OnSaleDate = obj.OnSaleDate;
        //              objFromDb.PageCount = obj.PageCount;
        //              objFromDb.Rated = obj.Rated;              
        //              objFromDb.TypeNewsId = obj.TypeNewsId;
        //              objFromDb.SeriesId = obj.SeriesId;
        //              objFromDb.Description = obj.Description;
        //              objFromDb.IsFree = obj.IsFree;
        //              objFromDb.IsNew = obj.IsNew;

        //              if (obj.Thumbnail !=null)
        //              {
        //                  objFromDb.Thumbnail = obj.Thumbnail;
        //              }            
        //              // Lưu thay đổi vào database
        //              _db.SaveChanges();
        //          }
        //          else
        //          {
        //              throw new Exception("News not found");
        //          }
        //      }
    }
}
