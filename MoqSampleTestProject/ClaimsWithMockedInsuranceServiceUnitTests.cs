using AutoMapper;
using Moq;
using MoqSample.DbEntities;
using MoqSample.DTOs.Request;
using MoqSample.Services;
using MoqSample.Services.InMemory;
using MoqSample.Tests.@base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Range = Moq.Range;

namespace MoqSample.Tests
{
    /// <summary>
    /// Units tests using Moq to mock an service
    /// 
    /// - Argument matching (Matching, not matching, in range, not in range)
    /// </summary>
    public class ClaimsWithMockedInsuranceServiceUnitTests: ClaimsWithMoqsUnitTestBase
    {
        IMapper mapper;
        public ClaimsWithMockedInsuranceServiceUnitTests()
        {
            mapper = AutoMapperConfig.Config();
        }

        [Fact]
        public void CreateClaimWithHttpDependency_NotImplementedException_Expected()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();

            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();

            //In this case we use the Http insurance service implementation (methods throw not implemented exception)
            var claimsController = ClaimsSampleFactory.CreateClaimsControllerWithHttpValidation(mapper, inmemoryContext);

            //Act, Assert
            Assert.Throws<NotImplementedException>(() => claimsController.CreateClaim(claimsRequest));
        }

        [Fact]
        public void CreateClaim_With_Null_Dependency_NotImplementedException_Expected()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();

            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();

            //In this case we use null as the insurace service implementation
            var claimsController = ClaimsSampleFactory.CreateClaimsControllerWithNullValidation(mapper, inmemoryContext);

            //Act, Assert
            Assert.Throws<NullReferenceException>(() => claimsController.CreateClaim(claimsRequest));
        }

        [Fact]
        public void CreateClaim_Mocked_InsuranceService_No_Matching_Arguments()
        {
            //Arrange
            var mockedService = new Mock<IInsuranceCompanyService>();

            //We setup the mock service, it returns true if the matching argument is 123
            mockedService.Setup(s => s.IsValidPolicyNumber("123")).Returns(true);

            //The argument doesn't match, therefore the mock returns the default value false
            var createFunction = CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(mapper, mockedService, "abc");

            //Act, Assert
            var exception = Assert.Throws<InvalidOperationException>(() => createFunction());
            Assert.Equal(Literals.Failed_Insurace_Policy_Number_Validation, exception.Message);
        }

        [Fact]
        public void CreateClaim_Mocked_InsuranceService_With_Matching_Arguments()
        {
            //Arrange
            var mockedService = new Mock<IInsuranceCompanyService>();

            //We setup the mock service, it returns true if the matching argument is 123
            mockedService.Setup(s => s.IsValidPolicyNumber("123")).Returns(true);

            bool isValid = true;
            mockedService.Setup(s => s.IsValidPolicyNumber(It.IsAny<string>(), out isValid));

            //The argument does match, therefore the mock returns true
            var createFunction = CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(mapper, mockedService, "123");

            //Act
            var response = createFunction();

            //Assert
            Assert.Equal(State.Open, response.State);
        }

        [Fact]
        public void CreateClaim_Mocked_InsuranceService_Matches_Any_Arguments()
        {
            //Arrange
            var mockedService = new Mock<IInsuranceCompanyService>();

            //We setup the mock service, it returns true for any string doesn't matter its value
            mockedService.Setup(s => s.IsValidPolicyNumber(It.IsAny<string>())).Returns(true);

            bool isValid = true;
            mockedService.Setup(s => s.IsValidPolicyNumber(It.IsAny<string>(), out isValid));

            //The argument does match, therefore the mock returns true
            var createFunction = CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(mapper, mockedService, "123");

            //Act
            var response = createFunction();

            //Assert
            Assert.Equal(State.Open, response.State);
        }

        [Fact]
        public void CreateClaim_Mocked_InsuranceService_Doesnt_Match_Arguments_In_A_Range()
        {
            //Arange
            var mockedService = new Mock<IInsuranceCompanyService>();

            //We setup the mock service, it returns true for any string that is in the range of a and g (excluding both)
            mockedService.Setup(s => s.IsValidPolicyNumber(It.IsInRange<string>("a", "g", Range.Exclusive))).Returns(true);

            //The argument doesn't match, it isn't in range, the validation fails and an exception is thrown
            var createFunction = CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(mapper, mockedService, "z");

            //Act, Assert
            //In this case we expect an exception as the policy number isn't valid
            var exception = Assert.Throws<InvalidOperationException>(() => createFunction());
            Assert.Equal(Literals.Failed_Insurace_Policy_Number_Validation, exception.Message);
        }

        [Fact]
        public void CreateClaim_Mocked_InsuranceService_Does_Match_Arguments_In_A_Range()
        {
            //Arrange
            var mockedService = new Mock<IInsuranceCompanyService>();

            //We setup the mock service, it returns true for any string that is in the range of a and g (excluding both)
            mockedService.Setup(s => s.IsValidPolicyNumber(It.IsInRange<string>("a", "g", Range.Exclusive))).Returns(true);

            bool isValid = true;
            mockedService.Setup(s => s.IsValidPolicyNumber(It.IsAny<string>(), out isValid));

            //The argument does match, it is in range, the validation doesn't fail and the claim is created with the correct state
            var createFunction = CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(mapper, mockedService, "b");

            //Act
            var response = createFunction();

            //Assert
            Assert.Equal(State.Open, response.State);
        }


        [Fact]
        public void CreateClaim_Mocked_InsuranceService_With_Out_Paremeter_Not_Setup()
        {
            //Arrange
            var mockedService = new Mock<IInsuranceCompanyService>();

            //We setup the mock service, it returns true if the matching argument is 123
            mockedService.Setup(s => s.IsValidPolicyNumber("123")).Returns(true);

            //The argument does match, therefore the mock returns true
            var createFunction = CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(mapper, mockedService, "123");

            //Act, Assert
            //In this case we expect an exception as the out parameter mock setup isn't prepared
            var exception = Assert.Throws<InvalidOperationException>(() => createFunction());
            Assert.Equal(Literals.Failed_Insurace_Policy_Number_Validation, exception.Message);
        }

        [Fact]
        public void CreateClaim_Mocked_InsuranceService_With_Out_Paremeter_Setup()
        {
            //Arrange
            var mockedService = new Mock<IInsuranceCompanyService>();

            //We setup the mock service, it returns true if the matching argument is 123
            mockedService.Setup(s => s.IsValidPolicyNumber("123")).Returns(true);

            bool isValid = true;
            mockedService.Setup(s => s.IsValidPolicyNumber("123", out isValid));

            //The argument does match, therefore the mock returns true
            var createFunction = CreateClaimWith_ArgumentMatching_Mocked_InsuranceService(mapper, mockedService, "123");

            //Act
            var response = createFunction();

            //Assert
            Assert.Equal(State.Open, response.State);
        }
    }
}
