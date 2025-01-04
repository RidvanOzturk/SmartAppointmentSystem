using Microsoft.AspNetCore.Mvc;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class UserController : Controller
    {

        [HttpPost]
        public IActionResult GetUser()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
