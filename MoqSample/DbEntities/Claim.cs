using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.DbEntities
{
    public class Claim
    {
        public Guid Id { get; set; }
        public PolicyHolder? PolicyHolder { get; set; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public State State { get; set; }
        public string? Description { get; set; }
    }
}
