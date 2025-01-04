using Microsoft.AspNetCore.Mvc;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
