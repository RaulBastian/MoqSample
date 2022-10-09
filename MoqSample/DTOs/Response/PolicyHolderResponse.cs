using MoqSample.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.DTOs.Response
{
    public class PolicyHolderResponse:PolicyHolderRequest
    {
        public Guid Id { get; set; }
    }
}
