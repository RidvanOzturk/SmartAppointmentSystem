using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Extensions;

public static class FunctionExtensions
{
    public static double CalculateAverageRating(ICollection<Rating> ratings)
    {
        if (ratings == null || !ratings.Any())
        {
            return 0;
        }
        return ratings.Average(r => r.Score);
    }
}
