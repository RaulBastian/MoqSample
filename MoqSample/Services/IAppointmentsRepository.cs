using MoqSample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.Services
{
    public interface IAppointmentsRepository
    {
        void Add(Appointment appointment);  

        Appointment? GetById(Guid id);

    }
}
