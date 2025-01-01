using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace HUUTRUNG.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
    {
        //(parameters) => expression
		//parameters: Danh sách các tham số đầu vào.
		//=>: Là toán tử lambda, đọc là "goes to".
		//expression: Là biểu thức hoặc khối mã được thực thi khi biểu thức lambda được gọi.
        private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
			_db = db;
			this.dbSet= _db.Set<T>();
			_db.Comics.Include(u => u.ComicCategory).Include(u=>u.ComicCategoryId);  // lấy thêm dữ liệu từ bảng TypeComic, nếu muốn lấy thêm bảng thì cứ Include
        }
        public void Add(T entity)
		{
			dbSet.Add(entity);
		}
		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
		{
            IQueryable<T> query;
            if (tracked){
                query = dbSet;
            }
			else{
                query = dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

		public IEnumerable<T> GetAll(string ? includeProperties=null)
		{
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in
					includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}

            }        
            if (dbSet == null)
			{
				throw new InvalidOperationException("The DbSet is not initialized.");
			}			
			return query.ToList();
		}
		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			
                query = query.Where(filter);
                   
            if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in
					includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}

			}
			if (dbSet == null)
			{
				throw new InvalidOperationException("The DbSet is not initialized.");
			}
		
			return query.ToList();
		}

		public PagedList<T> GetFilteredAndPaged(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int pageNumber = 1, int pageSize = 10, string? includeProperties = null)
		{
			// Bắt đầu truy vấn
			IQueryable<T> query = dbSet;

			// Bao gồm các thuộc tính liên quan (nếu có)
			if (!string.IsNullOrWhiteSpace(includeProperties))
			{
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			// Áp dụng bộ lọc
			if (filter != null)
			{
				query = query.Where(filter);
			}

			// Sắp xếp
			if (orderBy != null)
			{
				query = orderBy(query);
			}		
			// Phân trang
			//var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

			// Trả về PagedList
			return new PagedList<T>(query, pageNumber, pageSize);
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}			
		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}

	 


	}
}
