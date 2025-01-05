using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class RatingController(IRatingBusiness ratingBusiness) : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
