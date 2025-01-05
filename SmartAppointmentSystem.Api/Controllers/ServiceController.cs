using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class ServiceController(IServiceBusiness serviceBusiness) : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
