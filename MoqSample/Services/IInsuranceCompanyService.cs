using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.Services
{
    public interface IInsuranceCompanyService
    {
        bool IsValidPolicyNumber(string? policyNumber);


        void IsValidPolicyNumber(string? policyNumber, out bool isValid);
    }
}
