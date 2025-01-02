using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Data.Configurations
{
    public class TimeSlotConfiguration : EntityTypeConfiguration<TimeSlot>
    {
        public TimeSlotConfiguration()
        {
            ToTable("TimeSlots");
            HasKey(ts => ts.Id);

            Property(ts => ts.AvailableFrom)
                .IsRequired();

            Property(ts => ts.AvailableTo)
                .IsRequired();

            HasRequired(ts => ts.Service)
                .WithMany(s => s.TimeSlots)
                .HasForeignKey(ts => ts.ProfessionalId);
        }
    }
}
