using MoqSample.Services;
using MoqSample.Services.InMemory;
using System.Security.Claims;

namespace MoqSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mapper = AutoMapperConfig.Config();
            var inmemoryContext = new InMemoryContext();

            var claimsController = ClaimsSampleFactory.CreateClaimsController(mapper, inmemoryContext);
            var appointmentsController = ClaimsSampleFactory.CreateAppointmentsController(mapper, inmemoryContext);

            var claimRequest = ClaimsSampleFactory.CreatClaimRequest();
            var claimResponse = claimsController.CreateClaim(claimRequest);

            appointmentsController.CreateAppointment(claimResponse.Id, ClaimsSampleFactory.CreateRepairer(), DateTime.Today.AddDays(1));

        }
    }
}