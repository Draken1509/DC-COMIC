
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
    public class CompanyController : Controller
    {
        //private readonly ICompanyRepository _unitOfWork;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        public IActionResult Index()
        {
            List<Company> obj =
                _unitOfWork.Company.GetAll().ToList();  
            return View(obj);
        }

      
        public IActionResult Upsert(int? id)
        {
			if (id ==null || id==0)
			{
				return View(new Company());                
            }
            Company obj = _unitOfWork.Company.Get(u => u.Id == id);
            return View(obj);
        }

        [HttpPost]
		public IActionResult Upsert(Company obj,int? id)
		{			
			if (ModelState.IsValid)
			{						
				try{               					
						bool isNew = id == 0; // Flag để kiểm tra nếu là tạo mới hay cập nhật
						if (isNew){
							_unitOfWork.Company.Add(obj); // Tạo mới
						}
						else{
							_unitOfWork.Company.Update(obj); // Cập nhật
						}					
					 _unitOfWork.Save();  // Lưu vào cơ sở dữ liệu 													
					 TempData["success"] = isNew ? "Company created successfully" : "Company updated successfully"; //Thêm thông báo thành công vào TempData để hiển thị trong view
				}
			 catch (Exception ex){					
					TempData["error"] = "An error occurred while processing the request: " + ex.Message;
					return View(obj);
				}				
				return RedirectToAction("Index");
			}
			else{				
                return View(obj);
			}
		}


		#region api call
		[HttpGet]
        public IActionResult GetAll()
        {
			List<Company> obj =
			   _unitOfWork.Company.GetAll().ToList();
			return Json(new {data = obj });
		}

        public IActionResult Delete(int ?id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if(CompanyToBeDeleted==null)
            {
                return Json(new { success=false, message="Error while deleting" });
            }          
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();                      
			return Json(new { success = true, message = "Company deleted successfully" });
		}

        #endregion
    }
}
