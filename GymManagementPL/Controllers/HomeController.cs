using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        // Actions
        // Base URL /Home/Index
        // [NoAction]
        public ViewResult Index()
        {
            return View();
        }
    }
}
