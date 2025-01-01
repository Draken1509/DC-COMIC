
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.DataAccess.Data;

using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using HUUTRUNG.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Authorization;
using HUUTRUNG.Utility;
using HUUTRUNG.Models.ViewModel.Admin;
using HUUTRUNG.Models.Domain;

namespace HUUTRUNGWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles =SD.Role_Admin)]
    public class NewsController : Controller
    {
        //private readonly INewsRepository _unitOfWork;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NewsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<News> objTypeNewsList =
                _unitOfWork.News.GetAll(includeProperties: "NewsCategory").ToList(); 
            return View(objTypeNewsList);
        }
      
        public IActionResult Upsert(int? id)
        {
            NewsVMA newsVM = new()
            {
                TypeNewsList = _unitOfWork.NewsCategory
               .GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               }),               
                News = new News()
            };  
            
            if (id == null || id == 0){
                ViewData["PublishDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                return View(newsVM);
            }
            else{
                newsVM.News = _unitOfWork.News.Get(u => u.Id == id);
                if (newsVM.News.PublishDate != null){
                    ViewData["PublishDate"] = newsVM.News.PublishDate.ToString("yyyy-MM-dd");
                }
                else{
                    ViewData["PublishDate"] = "";

                }
                return View(newsVM);
            }
        }

        [HttpPost]
		public IActionResult Upsert(NewsVMA newsVM, IFormFile? file)
		    {			
			if (ModelState.IsValid)
			{			
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				try{               
					if (file != null)// Nếu có file được chọn	
					{   				 
						string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);// Tạo tên file ngẫu nhiên và lấy phần mở rộng của file					
						string NewsPath = Path.Combine(wwwRootPath, @"images\news");// Đường dẫn thư mục nơi lưu trữ ảnh					
						if (!string.IsNullOrEmpty(newsVM.News.Thumbnail)) // Kiểm tra nếu News đã có ảnh thumbnail cũ thì xóa ảnh cũ
						{
							var oldThumbnail =
								Path.Combine(wwwRootPath, newsVM.News.Thumbnail.TrimStart('\\'));
							if (System.IO.File.Exists(oldThumbnail)){
								System.IO.File.Delete(oldThumbnail);
							}
						}					
						using (var fileStream = new FileStream(Path.Combine(NewsPath, fileName), FileMode.Create)) // Lưu ảnh mới vào thư mục images/News
						{
							file.CopyTo(fileStream);
						}
                        newsVM.News.Thumbnail = @"images\news\" + fileName;// Lưu đường dẫn của ảnh vào thuộc tính Thumbnail của News
					}
					bool isNew = newsVM.News.Id == 0; // Flag để kiểm tra nếu là tạo mới hay cập nhật
					if (isNew){                                                   
						_unitOfWork.News.Add(newsVM.News); // Tạo mới
					}
					else{
						_unitOfWork.News.Update(newsVM.News); // Cập nhật
					}					
					 _unitOfWork.Save();  // Lưu vào cơ sở dữ liệu 													
					 TempData["success"] = isNew ? "News created successfully" : "News updated successfully"; //Thêm thông báo thành công vào TempData để hiển thị trong view
				}
			 catch (Exception ex){					
					TempData["error"] = "An error occurred while processing the request: " + ex.Message;
					return View(newsVM.News);
				}				
				return RedirectToAction("Index");
			}
			else{
                newsVM.TypeNewsList= _unitOfWork.NewsCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }); 
                return View(newsVM);
			}
		}



		#region api call
		[HttpGet]
        public IActionResult GetAll()
        {
			List<News> objTypeNewsList =
			   _unitOfWork.News.GetAll(includeProperties: "NewsCategory").ToList();
			return Json(new {data = objTypeNewsList });
		}

     

        public IActionResult Delete(int ?id)
        {
            var NewsToBeDeleted = _unitOfWork.News.Get(u => u.Id == id);
            if(NewsToBeDeleted==null)
            {
                return Json(new { success=false, message="Error while deleting" });
            }
            if (!NewsToBeDeleted.Thumbnail.IsNullOrEmpty())
            {
              var oldThumbnail =
              Path.Combine(_webHostEnvironment.WebRootPath,NewsToBeDeleted.Thumbnail.TrimStart('\\'));

                if (System.IO.File.Exists(oldThumbnail))
                {
                    System.IO.File.Delete(oldThumbnail);
                }
            }           
            _unitOfWork.News.Remove(NewsToBeDeleted);
            _unitOfWork.Save();                      
			return Json(new { success = true, message = "News deleted successfully" });
		}

        #endregion
    }
}
