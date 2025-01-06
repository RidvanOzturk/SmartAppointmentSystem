using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Implementations;

public class ServiceBusiness(IServiceRepository serviceRepository) : IServiceBusiness
{
}
