using MoqSample.Services.InMemory;
using MoqSample;
using MoqSample.DbEntities;
using AutoMapper;
using Xunit;

namespace MoqSample.Tests
{
    public class ClaimsUnitTests
    {
        IMapper mapper;
        public ClaimsUnitTests()
        {
            mapper = AutoMapperConfig.Config();
        }

        [Fact]
        public void CreateClaim()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();

            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();
            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper,inmemoryContext);

            //Act
            var claim = claimsController.CreateClaim(claimsRequest);

            //Assert
            Assert.Equal<State>(MoqSample.DbEntities.State.Open,claim.State);
        }

        [Fact]
        public void CreateClaimExpectedNullException()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();
            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper, inmemoryContext);

            //Act, Assert
            var exception = Assert.Throws<ArgumentNullException>(() => claimsController.CreateClaim(null));
            Assert.Contains(Literals.Claims_Request_Required, exception.Message);
        }

        [Fact]
        public void CreateClaimNoPolicyHolder_ArgumentNullExceptionExpected()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();
            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper, inmemoryContext);
            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();
            claimsRequest.PolicyHolder = null;

            //Act, Assert
            var exception = Assert.Throws<ArgumentNullException>(() => claimsController.CreateClaim(claimsRequest));
            Assert.Contains(Literals.Policy_Holder_Required, exception.Message);
        }

        [Fact]
        public void CreateClaimNoPolicyHolderNumer_InvalidOperationExceptionExpected()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();
            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper, inmemoryContext);
            
            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();
            if(claimsRequest.PolicyHolder != null)
            {
                claimsRequest.PolicyHolder.PolicyNumber = String.Empty;
            }

            //Act, Assert
            var exception = Assert.Throws<InvalidOperationException>(() => claimsController.CreateClaim(claimsRequest));
            Assert.Equal(Literals.Policy_Number_Required, exception.Message);
        }

        [Fact]
        public void ChangeClaimStateInvalidClaimsId_ArgumentExceptionExpected()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();
            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper, inmemoryContext);

            //Act, Assert
           var exception = Assert.Throws<ArgumentException>(() => claimsController.ChangeState(Guid.NewGuid(), State.InProgress));
           Assert.Equal(Literals.Claims_Id_doesnt_Exist, exception.Message);
        }

        [Fact]
        public void ChangeClaimStateNoAppointmentsWrongTargetState_InvalidOperationExceptionExpected()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();

            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper, inmemoryContext);
            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();
            var claimsReponse = claimsController.CreateClaim(claimsRequest);

            //Act, Assert
           var exception = Assert.Throws<InvalidOperationException>(() => claimsController.ChangeState(claimsReponse.Id, State.InProgress));
        }

        [Fact]
        public void ChangeClaimStateWithAppointmentsToInProgress()
        {
            //Arrange
            var inmemoryContext = new InMemoryContext();

            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper, inmemoryContext);
            var appointmentsController = ClaimsSampleFactory.CreateAppointmentsController(mapper, inmemoryContext);

            var claimsRequest = ClaimsSampleFactory.CreatClaimRequest();
            var claimsResponse = claimsController.CreateClaim(claimsRequest);

            //Act
            var appointment = appointmentsController.CreateAppointment(claimsResponse.Id, ClaimsSampleFactory.CreateRepairer(), DateTime.Now.AddDays(1));

            claimsController.ChangeState(claimsResponse.Id, State.InProgress);

            var claimsResponseWithNewState= claimsController.GetClaimById(claimsResponse.Id);

            //Assert
            Assert.Equal<State>(State.InProgress, claimsResponseWithNewState.State);
        }
    }
}