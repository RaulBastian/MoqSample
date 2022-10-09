using MoqSample.DTOs.Request;
using MoqSample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.DTOs.Response
{
    public class ClaimsResponse
    {
        public Guid Id { get; set; }

        public PolicyHolderResponse? PolicyHolder { get; set; }
        public List<AppointmentResponse> Appointments { get; set; } = new List<AppointmentResponse>();
        public List<InvoiceResponse> Invoices { get; set; } = new List<InvoiceResponse>();
        public List<ExpenseResponse> Expenses { get; set; } = new List<ExpenseResponse>();
        public State State { get; set; }
        public string? Description { get; set; }
    }
}
