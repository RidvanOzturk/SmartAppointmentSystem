using SmartAppointmentSystem.Data.Entities;
using SmartAppointmentSystem.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Data.Repositories.Implementations;

public class UserRepository(AppointmentContext context) : IUserRepository
{
    public async Task<User?> GetByMailandNameAsync(string name, string mail)
    {
        return context.Users.FirstOrDefault(x => x.Name == name || x.Mail == mail);
    }
}
