using MoqSample.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.DTOs.Response
{
    public class ExpenseResponse:ExpenseRequest
    {
        public Guid Id { get; set; }
    }
}
