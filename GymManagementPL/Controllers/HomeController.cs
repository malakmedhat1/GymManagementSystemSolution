using GymManagmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticService _analyticService;

        // Actions
        // Base URL /Home/Index
        // [NoAction]

        public HomeController(IAnalyticService analyticService)
        {
            _analyticService = analyticService;
        }
        public ViewResult Index()
        {
            var analyticsData = _analyticService.GetAnalyticsData();
            return View(analyticsData);
        }
    }
}
