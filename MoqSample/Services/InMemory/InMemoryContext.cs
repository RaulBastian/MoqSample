using Microsoft.EntityFrameworkCore;
using MoqSample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.Services.InMemory
{
    public class InMemoryContext:DbContext
    {
        public List<Claim> Claims = new List<Claim>();

        public List<Appointment> Appointments = new List<Appointment>();
    }
}
