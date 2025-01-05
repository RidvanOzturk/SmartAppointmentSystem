﻿using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers
{
    public class AppointmentController(IAppointmentBusiness appointmentBusiness) : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
