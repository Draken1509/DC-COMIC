using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUUTRUNGWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CharacterController : Controller
    {
		//private readonly ICharacterRepository _unitOfWork;
		private readonly IUnitOfWork _unitOfWork;
		public CharacterController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			var objCharacterList = _unitOfWork.Character.GetAll().ToList();
			return View(objCharacterList);
		}

		// action name : create
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Character obj)
		{
			//if (obj.Name == obj.DisplayOrder.ToString())
			//{
			//	ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name ");
			//}
			if (ModelState.IsValid)
			{
				_unitOfWork.Character.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Character created successfully";
				return RedirectToAction("Index");
			}
			return View();

		}

		//action name: edit
		public IActionResult Edit(int? id)  //? can null
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Character? CharacterFromDb = _unitOfWork.Character.Get(u => u.Id == id);
			if (CharacterFromDb == null)
			{
				return NotFound();
			}
			return View(CharacterFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Character obj)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Character.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Character edit successfully";
				return RedirectToAction("Index");
			}
			return View();
		}


		//action name: delete
		public IActionResult Delete(int? id)  //? can null
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Character? CharacterFromDb = _unitOfWork.Character.Get(u => u.Id == id);
			if (CharacterFromDb == null)
			{
				return NotFound();
			}

			return View(CharacterFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Character? obj = _unitOfWork.Character.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitOfWork.Character.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Character delete successfully";
			return RedirectToAction("Index");
		}
	}
}
