using MoqSample.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.DTOs.Response
{
    public class AppointmentResponse: AppointmentRequest
    {
        public Guid Id { get; set; }
    }
}
