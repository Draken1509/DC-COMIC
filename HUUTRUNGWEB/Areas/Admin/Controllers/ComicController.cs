
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
using HUUTRUNG.Models.Domain;

namespace HUUTRUNGWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles =SD.Role_Admin)]
    public class ComicController : Controller
    {
        //private readonly IComicRepository _unitOfWork;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ComicController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Comic> objTypeComicList =
                _unitOfWork.Comic.GetAll(includeProperties: "ComicCategory").ToList();  
            return View(objTypeComicList);
        }
      
        public IActionResult Upsert(int? id)
        {
            //ViewBag.TypeComicList = TypeComicList
            //ViewData["TypeComicList"]= TypeComicList

            ComicVM comicVM = new()
            {
                TypeComicList = _unitOfWork.ComicCategory
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                SeriesList = _unitOfWork.Series
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Comic = new Comic()			
            };
            if (id == null || id == 0)
            {
                return View(comicVM);
            }
            else
            {
                comicVM.Comic = _unitOfWork.Comic.Get(u => u.Id == id);
				if (comicVM.Comic.OnSaleDate != null)
				{
                    ViewData["OnSaleDate"] = comicVM.Comic.OnSaleDate.Value.ToString("yyyy-MM-dd");
                }
				else
				{
					ViewData["OnSaleDate"] = "";

                }
       
                //comicVM.Comic.OnSaleDate = comicVM.OnSaleDate.ToString("dd/MM/yyyy");
                return View(comicVM);
            }
        }

        [HttpPost]
		public IActionResult Upsert(ComicVM comicVM, IFormFile? file)
		{			
			if (ModelState.IsValid)
			{			
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				try{               
					if (file != null)// Nếu có file được chọn	
					{   				 
						string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);// Tạo tên file ngẫu nhiên và lấy phần mở rộng của file					
						string comicPath = Path.Combine(wwwRootPath, @"images\comic");// Đường dẫn thư mục nơi lưu trữ ảnh					
						if (!string.IsNullOrEmpty(comicVM.Comic.Thumbnail)) // Kiểm tra nếu comic đã có ảnh thumbnail cũ thì xóa ảnh cũ
						{
							var oldThumbnail =
								Path.Combine(wwwRootPath, comicVM.Comic.Thumbnail.TrimStart('\\'));
							if (System.IO.File.Exists(oldThumbnail)){
								System.IO.File.Delete(oldThumbnail);
							}
						}					
						using (var fileStream = new FileStream(Path.Combine(comicPath, fileName), FileMode.Create)) // Lưu ảnh mới vào thư mục images/comic
						{
							file.CopyTo(fileStream);
						}						
						comicVM.Comic.Thumbnail = @"images\comic\" + fileName;// Lưu đường dẫn của ảnh vào thuộc tính Thumbnail của comic
					}
						bool isNew = comicVM.Comic.Id == 0; // Flag để kiểm tra nếu là tạo mới hay cập nhật
						if (isNew){
							_unitOfWork.Comic.Add(comicVM.Comic); // Tạo mới
						}
						else{
							_unitOfWork.Comic.Update(comicVM.Comic); // Cập nhật
						}					
					 _unitOfWork.Save();  // Lưu vào cơ sở dữ liệu 													
					 TempData["success"] = isNew ? "Comic created successfully" : "Comic updated successfully"; //Thêm thông báo thành công vào TempData để hiển thị trong view
				}
			 catch (Exception ex){					
					TempData["error"] = "An error occurred while processing the request: " + ex.Message;
					return View(comicVM);
				}				
				return RedirectToAction("Index");
			}
			else{
				// Nếu ModelState không hợp lệ, tải lại danh sách các thể loại comic để hiển thị lại form
				comicVM.TypeComicList = _unitOfWork.ComicCategory.GetAll().Select(u => new SelectListItem{
					Text = u.Name,
					Value = u.Id.ToString()
				}); // Trả về lại view với model comicVM chứa danh sách các thể loại
                comicVM.SeriesList = _unitOfWork.Series.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(comicVM);
			}
		}

		//action name: edit
		//public IActionResult Edit(int? id)  //? can null
		//{
		//    if (id == null || id == 0)
		//    {
		//        return NotFound();
		//    }
		//    Comic? ComicFromDb = _unitOfWork.Comic.Get(u => u.Id == id);
		//    if (ComicFromDb == null)
		//    {
		//        return NotFound();
		//    }
		//    return View(ComicFromDb);
		//}
		//[HttpPost]
		//public IActionResult Edit(Comic obj)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        _unitOfWork.Comic.Update(obj);
		//        _unitOfWork.Save();
		//        TempData["success"] = "Comic edit successfully";
		//        return RedirectToAction("Index");
		//    }
		//    return View();
		//}

		//action name: delete
		//public IActionResult Delete(int? id)  //? can null
		//{
		//    if (id == null || id == 0)
		//    {
		//        return NotFound();
		//    }
		//    Comic? ComicFromDb = _unitOfWork.Comic.Get(u => u.Id == id);
		//    if (ComicFromDb == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(ComicFromDb);
		//}
		//[HttpPost, ActionName("Delete")]
		//public IActionResult DeletePOST(int? id)
		//{
		//    Comic? obj = _unitOfWork.Comic.Get(u => u.Id == id);
		//    if (obj == null)
		//    {
		//        return NotFound();
		//    }
		//    _unitOfWork.Comic.Remove(obj);
		//    _unitOfWork.Save();
		//    TempData["success"] = "Comic delete successfully";
		//    return RedirectToAction("Index");
		//}

		#region api call
		[HttpGet]
        public IActionResult GetAll()
        {
			List<Comic> objTypeComicList =
			   _unitOfWork.Comic.GetAll(includeProperties: "ComicCategory").ToList();
			return Json(new {data = objTypeComicList });
		}

        [HttpPost]
        public IActionResult UpdateIsNew(int id, bool isNew)
        {
			Comic comic = _unitOfWork.Comic.Get(u => u.Id == id);
            if (comic != null)
            {
                comic.IsNew = isNew;
				_unitOfWork.Save();
                return Json(new { success = true, message = "Updated successfully" });
            }
            return Json(new { success = false, message = "Comic not found" });
        }

        public IActionResult Delete(int ?id)
        {
            var comicToBeDeleted = _unitOfWork.Comic.Get(u => u.Id == id);
            if(comicToBeDeleted==null)
            {
                return Json(new { success=false, message="Error while deleting" });
            }
            if (!comicToBeDeleted.Thumbnail.IsNullOrEmpty())
            {
              var oldThumbnail =
              Path.Combine(_webHostEnvironment.WebRootPath,comicToBeDeleted.Thumbnail.TrimStart('\\'));

                if (System.IO.File.Exists(oldThumbnail))
                {
                    System.IO.File.Delete(oldThumbnail);
                }
            }           
            _unitOfWork.Comic.Remove(comicToBeDeleted);
            _unitOfWork.Save();                      
			return Json(new { success = true, message = "Comic deleted successfully" });
		}

        #endregion
    }
}
