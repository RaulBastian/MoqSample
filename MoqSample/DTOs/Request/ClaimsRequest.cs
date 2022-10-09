using MoqSample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.DTOs.Request
{
    public class ClaimsRequest
    {
        public PolicyHolderRequest? PolicyHolder { get; set; }
        public List<AppointmentRequest> Appointments { get; set; } = new List<AppointmentRequest>();
        public List<InvoiceRequest> Invoices { get; set; } = new List<InvoiceRequest>();
        public List<ExpenseRequest> Expenses { get; set; } = new List<ExpenseRequest>();
        public State State { get; set; }
        public string? Description { get; set; }
    }
}
