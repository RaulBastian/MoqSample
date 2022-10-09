using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.DbEntities
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Claim? Claim { get; set; }

        public DateTime DateAndTime { get; set; }
        public string? Title { get; set; }
        public Repairer? Repairer { get; set; }

        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }
    }
}
