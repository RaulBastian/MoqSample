using MoqSample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.Services.InMemory
{
    public class AppointmentsInMemoryRepository : IAppointmentsRepository
    {
        private readonly InMemoryContext inMemoryContext;

        public AppointmentsInMemoryRepository(InMemoryContext inMemoryContext)
        {
            this.inMemoryContext = inMemoryContext;
        }

        public void Add(Appointment appointment)
        {
            appointment.Id = Guid.NewGuid();
            this.inMemoryContext.Appointments.Add(appointment);
        }

        public Appointment? GetById(Guid id)
        {
            return this.inMemoryContext.Appointments.Where(a=> a.Id == id).FirstOrDefault();
        }
    }
}
