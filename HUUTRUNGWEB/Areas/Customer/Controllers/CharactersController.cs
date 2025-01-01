using Microsoft.AspNetCore.Mvc;

namespace HUUTRUNGWEB.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CharactersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DetailCharacter()
        {
            return View();
        }
    }

}
