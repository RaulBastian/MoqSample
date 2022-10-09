using AutoMapper;
using Moq;
using MoqSample.Services.InMemory;
using MoqSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MoqSample.DbEntities;
using MoqSample.DTOs.Response;

namespace MoqSample.Tests.@base
{
    public class ClaimsWithMoqsUnitTestBase
    {
        protected Func<ClaimsResponse> CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(IMapper mapper, Mock<IInsuranceCompanyService> mockedInsuranceService, string policyNumber)
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();

            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();

            if (claimsRequest.PolicyHolder == null)
            {
                throw new ArgumentNullException(nameof(claimsRequest.PolicyHolder));
            }

            claimsRequest.PolicyHolder.PolicyNumber = policyNumber;


            var controller = ClaimsSampleFactory.CreateClaimsControllerWithValidationParameter(mapper, inmemoryContext, mockedInsuranceService.Object);
            return () => { return controller.CreateClaim(claimsRequest); };
        }
    }
}
