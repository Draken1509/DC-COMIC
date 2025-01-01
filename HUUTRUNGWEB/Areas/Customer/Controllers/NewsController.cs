using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.ViewModel.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace HUUTRUNGWEB.Areas.Customer.Controllers
{
    [Area("Customer")]
	public class NewsController : Controller
	{
		private readonly ILogger<NewsController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		public NewsController(ILogger<NewsController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			List<News> newList = _unitOfWork.News.GetAll().ToList();
			NewsVM newsVM = new()
			{
				OffcialAndLatestNewsList = newList
				.OrderByDescending(n => n.PublishDate).Take(5).ToList(),

				FeaturesNewsList = newList
				.Where(n => n.NewsCategoryId == 1)
				.OrderByDescending(n => n.PublishDate)
				.Take(5)
				.ToList(),

				InterviewsNewsList = newList
				.Where(n => n.NewsCategoryId == 2)
				.OrderByDescending(n => n.PublishDate)
				.Take(5)
				.ToList()
			};		
			return View(newsVM);
		}
        public IActionResult Details(int? newsId)
        {
            News News = _unitOfWork.News.Get(u => u.Id == newsId);           
            return View(News);
        }
    }
}


// OFFICIAL DC NEWS       :5 tin tức mới nhất
// LATEST NEWS            :5 tin tức mới nhất

// INTERVIEWS NEWS        :3 tin tức kiểu FEATURES
// FEATURES	NEWS		  :4 tin tức kiểu INTERVIEWS