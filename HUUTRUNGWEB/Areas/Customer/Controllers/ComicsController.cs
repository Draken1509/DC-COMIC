using HUUTRUNG.DataAccess.Repository;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.ViewModel.Admin;
using HUUTRUNG.Models.ViewModel.Customer;
using HUUTRUNG.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using X.PagedList;
using System.Drawing.Printing;
using System.Globalization;
using System.Security.Claims;
using X.PagedList.Extensions;
using System;
using System.Linq.Expressions;
using HUUTRUNG.Models.Domain;


namespace HUUTRUNGWEB.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ComicsController : Controller
    {
		private readonly ILogger<ComicsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ComicsController(ILogger<ComicsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

		#region dung mo
		//[HttpPost]
		//public IActionResult Index(
		//       int? page = 1,
		//       string? filterName = null,
		//       string? filterType = null,
		//       string? filterWriter = null
		//      )
		//{
		//	int pageSize = 12;
		//	int pageNumber = page ?? 1; // Nếu không có page thì mặc định là 1

		//	var comicList = _unitOfWork.Comic.GetAll(includeProperties: "TypeComic").ToList();
		//	var comicListIntial = comicList;

		//	// Lọc theo filterName
		//	if (!string.IsNullOrWhiteSpace(filterName))
		//	{
		//		comicList = comicList.Where(x =>
		//			x.Name != null && x.Name.Contains(filterName, StringComparison.OrdinalIgnoreCase)
		//		).ToList();
		//	}

		//	// Lọc theo filterWriter và filterType, nếu có
		//	if (!string.IsNullOrWhiteSpace(filterWriter) && !string.IsNullOrWhiteSpace(filterType))
		//	{
		//		// Lọc theo cả tác giả và thể loại
		//		comicList = comicList.Where(x =>
		//			(x.Writer != null && x.Writer.Contains(filterWriter, StringComparison.OrdinalIgnoreCase)) &&
		//			(x.TypeComic != null && x.TypeComic.Name.Contains(filterType, StringComparison.OrdinalIgnoreCase))
		//		).ToList();
		//	}
		//	else if (!string.IsNullOrWhiteSpace(filterWriter))
		//	{
		//		// Lọc theo tác giả
		//		comicList = comicList.Where(x =>
		//			x.Writer != null && x.Writer.Contains(filterWriter, StringComparison.OrdinalIgnoreCase)
		//		).ToList();
		//	}
		//	else if (!string.IsNullOrWhiteSpace(filterType))
		//	{
		//		// Lọc theo thể loại
		//		comicList = comicList.Where(x =>
		//			x.TypeComic != null && x.TypeComic.Name.Contains(filterType, StringComparison.OrdinalIgnoreCase)
		//		).ToList();
		//	}

		//	// Lọc thêm các bộ lọc khác nếu có

		//	// Phân trang
		//	var comicPagedList = new PagedList<Comic>(comicList, pageNumber, pageSize);

		//          // Tạo ViewModel
		//          ShoppingCartVM shoppingCartVM = new()
		//	{             
		//ComicFreeList = comicListIntial.Where(c => c.IsFree == true).ToList(),
		//		ComicNewList =  comicListIntial.Where(c => c.IsNew == true).ToList(),
		//             // ComicRelatedSeriesList = comicListIntial.Where(c => c.SeriesId == ).ToList(),             
		//              ComicList = comicList,
		//		ComicListPagination = comicPagedList,
		//		TypeComicList = _unitOfWork.TypeComic.GetAll().ToList(),
		//		WriterComicList = comicListIntial.Select(c => c.Writer).Distinct().ToList()
		//	};

		//	return View(shoppingCartVM);
		//}
		#endregion

		public IActionResult Index(
			string? search =null, // Tìm kiếm
			string? sortBy = null,     // Sắp xếp
			string? filterType = null, // Lọc theo TypeComic
			string? filterWriter = null, // Lọc theo Writer
			int? page = 1       // Phân trang
		)
		{		
			int pageSize = 8;
			int pageNumber = page ?? 1;
			// Lấy danh sách comics đã xử lý từ repository
			var comicList = _unitOfWork.Comic.GetAll(includeProperties: "ComicCategory").ToList();
			var pagedComics = _unitOfWork.Comic.GetComics(
			search: search,
			filterType: filterType,
			filterWriter: filterWriter,
			sortBy: sortBy,
			pageNumber: pageNumber,
			pageSize: pageSize
		);

		// Truyền dữ liệu vào ViewModel nếu cần
		ShoppingCartVM shoppingCartVM = new()
		{
			ComicListPagination = pagedComics,
			TypeComicList = _unitOfWork.ComicCategory.GetAll().ToList(),
			WriterComicList = _unitOfWork.Comic.GetAll(c => c.Writer != null).Select( c=>c.Writer).Distinct().ToList(),
		    ComicFreeList = _unitOfWork.Comic.GetAll(c => c.IsFree == true).ToList(),
			ComicNewList = _unitOfWork.Comic.GetAll(c => c.IsNew == true).ToList(),	
			MovieMain = _unitOfWork.Movie.GetAll(u => u.IsMain == true)  
	                                         .OrderBy(u => u.Id)             
	                                         .Skip(2)                      
	                                         .Take(1)                        
	                                         .FirstOrDefault(),
			ComicList = comicList,							
		};

		return View(shoppingCartVM);
	}

		public IActionResult Details(int ? comicId) {
            Comic comic = _unitOfWork.Comic.Get(u => u.Id == comicId, includeProperties: "ComicCategory");        
            ShoppingCartVM shoppingCartVM = new()
            {
                ComicRelatedSeriesList = _unitOfWork.Comic.GetAll(c => c.SeriesId == comic.SeriesId)               
                .Select(c => new Comic
                {
                    Id = c.Id,
                    Price = c.Price,
                    Thumbnail = c.Thumbnail,
                    Name = c.Name,
                })
                .OrderBy(c => c.Name) // Sắp xếp theo tên
                .ToList(),

                ShoppingCart = new()
                {
                    Comic = comic,
                    Count = 1,
                    ComicId = comicId ?? 0
                }
            };
            return View(shoppingCartVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCartVM shoppingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCartVM.ShoppingCart.ApplicationUserId = userId;
            ShoppingCart cartFromDb =
                 _unitOfWork
                .ShoppingCart
                .Get(u => u.ApplicationUserId == userId && u.ComicId == shoppingCartVM.ShoppingCart.ComicId);

            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCartVM.ShoppingCart.Count;
               // _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingCartVM.ShoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
               _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
          
            TempData["success"] = "Cart updated successfully!";

            return RedirectToAction(nameof(Index));
        }

    }


}

