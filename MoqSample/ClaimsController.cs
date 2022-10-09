using AutoMapper;
using MoqSample.DTOs.Request;
using MoqSample.DTOs.Response;
using MoqSample.DbEntities;
using MoqSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample
{
    public class ClaimsController
    {
        private readonly IMapper mapper;
        private readonly IClaimsRepository claimsRepository;
        private readonly IInsuranceCompanyService? insuranceCompanyService;

        public ClaimsController(IMapper mapper, IClaimsRepository claimsRepository, IInsuranceCompanyService? insuranceCompanyService)
        {
            this.mapper = mapper;
            this.claimsRepository = claimsRepository;
            this.insuranceCompanyService = insuranceCompanyService;
        }

        public ClaimsResponse CreateClaim(ClaimsRequest? claimRequest)
        {
            if(claimRequest == null)
                throw new ArgumentNullException(Literals.Claims_Request_Required);

            if(claimRequest.PolicyHolder == null)
                throw new ArgumentNullException(Literals.Policy_Holder_Required);

            if(string.IsNullOrEmpty(claimRequest.PolicyHolder.PolicyNumber))
                throw new InvalidOperationException(Literals.Policy_Number_Required);

            if (insuranceCompanyService == null)
                throw new NullReferenceException(nameof(insuranceCompanyService));

            //We validate the policy number
            if(!insuranceCompanyService.IsValidPolicyNumber(claimRequest.PolicyHolder.PolicyNumber))
                throw new InvalidOperationException(Literals.Failed_Insurace_Policy_Number_Validation);

            //We validate again but with an out parameter
            bool isValidPolicyNumber;
            insuranceCompanyService.IsValidPolicyNumber(claimRequest.PolicyHolder.PolicyNumber, out isValidPolicyNumber);

            if (!isValidPolicyNumber)
                throw new InvalidOperationException(Literals.Failed_Insurace_Policy_Number_Validation);

            claimRequest.State = State.Open;
            var claim = this.claimsRepository.Create(mapper.Map<Claim>(claimRequest));
            
            return mapper.Map<ClaimsResponse>(claim);
        }

        public ClaimsResponse GetClaimById(Guid id)
        {
            var claim = this.claimsRepository.GetById(id);

            if (claim == null)
            {
                throw new ArgumentException(Literals.Claims_Id_doesnt_Exist);
            }

            return mapper.Map<ClaimsResponse>(claim);
        }

        public void ChangeState(Guid id, State state)
        {
            var claim = this.claimsRepository.GetById(id);

            if(claim == null)
            {
                throw new ArgumentException(Literals.Claims_Id_doesnt_Exist);
            }

            //We can only change to in progress if it has open appointments
            if(state == State.InProgress && !claim.Appointments.Where(a=> !a.IsFinished).Any())
            {
                throw new InvalidOperationException(Literals.Open_Appointments_Required);
            }

            //We can only change to in progress if it has closed appointments
            if (state == State.OpenInvoices && claim.Appointments.Where(a => !a.IsFinished).Any())
            {
                throw new InvalidOperationException(Literals.All_Appointments_Must_Be_Finished);
            }

            if(state == State.Completed && claim.Invoices.Where(i => !i.IsPaid).Any())
            {
                throw new InvalidOperationException(Literals.All_Invoices_Must_Be_Paid);
            }


            claim.State = state;
        }
    }
}
