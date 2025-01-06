using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Data.Repositories.Contracts;

public interface IUserRepository
{
    Task<User?> GetByMailandNameAsync(string name, string mail);
}
