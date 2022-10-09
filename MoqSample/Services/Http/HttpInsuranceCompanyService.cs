using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.Services.Http
{
    public class HttpInsuranceCompanyService : IInsuranceCompanyService
    {
        public bool IsValidPolicyNumber(string? policyNumber)
        {
            throw new NotImplementedException("Simulates an HTTP external service call");
        }

        public void IsValidPolicyNumber(string? policyNumber, out bool isValid)
        {
            throw new NotImplementedException("Simulates an HTTP external service call");
        }
    }
}
