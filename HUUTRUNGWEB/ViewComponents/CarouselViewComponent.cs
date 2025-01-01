using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HUUTRUNGWEB.ViewComponents
{
	public class CarouselViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;
		public CarouselViewComponent(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			Random random = new Random();
			var NewsRandom = _unitOfWork.News.GetAll().OrderBy(x => random.Next()).Take(3).ToList();
			return View(NewsRandom);
		}
	}
}
