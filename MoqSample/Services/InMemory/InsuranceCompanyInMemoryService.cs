using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.Services.InMemory
{
    public class InsuranceCompanyInMemoryService : IInsuranceCompanyService
    {
        public bool IsValidPolicyNumber(string? policyNumber)
        {
            return true;
        }

        public void IsValidPolicyNumber(string? policyNumber, out bool isValid)
        {
            isValid = true;
        }
    }
}
