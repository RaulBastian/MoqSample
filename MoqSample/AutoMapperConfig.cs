using AutoMapper;
using MoqSample.DTOs.Request;
using MoqSample.DTOs.Response;
using MoqSample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample
{
    public static class AutoMapperConfig
    {

        public static IMapper Config()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ClaimsRequest, Claim>();
                cfg.CreateMap<AppointmentRequest, Appointment>();
                cfg.CreateMap<ExpenseRequest, Expense>();
                cfg.CreateMap<InvoiceRequest, Invoice>();
                cfg.CreateMap<PolicyHolderRequest, PolicyHolder>();
                
                
                cfg.CreateMap<Claim, ClaimsResponse>();
                cfg.CreateMap<Appointment, AppointmentResponse>();
                cfg.CreateMap<Expense, ExpenseResponse>();
                cfg.CreateMap<Invoice, InvoiceResponse>();
                cfg.CreateMap<PolicyHolder, PolicyHolderResponse>();

            });
            return config.CreateMapper();
        }
    }
}
