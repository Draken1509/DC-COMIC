using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HUUTRUNGWEB.ViewComponents
{
	public class SwiperViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;
		public SwiperViewComponent(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			Random random = new Random();
			var latestNewsList = _unitOfWork.News.GetAll(u => u.NewsCategory.Name == "COMIC NEWS")
				.OrderByDescending(x => x.PublishDate)  
				.Take(6)  
				.Select(x => new News
				{
					Tittle=x.Tittle,      
					Thumbnail= x.Thumbnail,  
					Id = x.Id       
				})
				.ToList();



			return View(latestNewsList);
		}
	}
}
