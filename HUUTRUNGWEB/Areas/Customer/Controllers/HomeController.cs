using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.ViewModel.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HUUTRUNGWEB.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

		}

        public IActionResult Index()
        {
            var movieMain = _unitOfWork.Movie.GetAll(u => u.IsMain == true)  
	                                         .OrderBy(u => u.Id)             
	                                         .Skip(1)                      
	                                         .Take(1)                        
	                                         .FirstOrDefault();
            var comicFreeList = _unitOfWork.Comic.GetAll(c => c.IsFree == true).ToList();
            HomeVM homeVM = new HomeVM()
            {
                MainVideo = movieMain,
                ComicFreeList = comicFreeList,
            };
			return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
