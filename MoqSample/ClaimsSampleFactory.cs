using AutoMapper;
using MoqSample.DTOs.Request;
using MoqSample.DbEntities;
using MoqSample.Services;
using MoqSample.Services.Http;
using MoqSample.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample
{
    public class ClaimsSampleFactory
    {
        public static ClaimsController CreateClaimsControllerWithHttpValidation(IMapper mapper, InMemoryContext inMemoryContext)
        {
            var insuranceService = new HttpInsuranceCompanyService();
            var claimsRepository = new ClaimsInMemoryRepository(inMemoryContext);
            var appointmentsRepository = new AppointmentsInMemoryRepository(inMemoryContext);

            return new ClaimsController(mapper, claimsRepository, insuranceService);
        }

        public static ClaimsController CreateClaimsControllerWithNullValidation(IMapper mapper, InMemoryContext inMemoryContext)
        {
            var claimsRepository = new ClaimsInMemoryRepository(inMemoryContext);
            var appointmentsRepository = new AppointmentsInMemoryRepository(inMemoryContext);

            return new ClaimsController(mapper, claimsRepository, null);
        }

        public static ClaimsController CreateClaimsControllerWithValidationParameter(IMapper mapper, InMemoryContext inMemoryContext, IInsuranceCompanyService insuranceCompanyService)
        {
            var claimsRepository = new ClaimsInMemoryRepository(inMemoryContext);
            var appointmentsRepository = new AppointmentsInMemoryRepository(inMemoryContext);

            return new ClaimsController(mapper, claimsRepository, insuranceCompanyService);
        }

        public static ClaimsController CreateClaimsController(IMapper mapper, InMemoryContext inMemoryContext)
        {
            var insuranceService = new InsuranceCompanyInMemoryService();
            var claimsRepository = new ClaimsInMemoryRepository(inMemoryContext);
            var appointmentsRepository = new AppointmentsInMemoryRepository(inMemoryContext);

            return new ClaimsController(mapper, claimsRepository, insuranceService);
        }

        public static AppointmentsController CreateAppointmentsController(IMapper mapper, InMemoryContext inMemoryContext)
        {
            var insuranceService = new InsuranceCompanyInMemoryService();
            var appointmentsRepository = new AppointmentsInMemoryRepository(inMemoryContext);
            var claimsRepository = new ClaimsInMemoryRepository(inMemoryContext);

            return new AppointmentsController(mapper, appointmentsRepository, claimsRepository);
        }


        public static ClaimsRequest CreatClaimRequest()
        {
            var c = new ClaimsRequest();
            c.PolicyHolder = CreatePolicyHolder();
            return c;
        }

        private static PolicyHolderRequest CreatePolicyHolder()
        {
            var p = new PolicyHolderRequest();
            p.Address = "123 new road";
            p.Name = "Fred";
            p.Surname = "Peterson";
            p.PolicyNumber = "AA123";
            return p;
        }

        public static Repairer CreateRepairer()
        {
            var r = new Repairer();
            r.Trade = Trade.Painter;
            r.Name = "Pepe";
            return r;
        }

    }
}
