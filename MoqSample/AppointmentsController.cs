using AutoMapper;
using MoqSample.DbEntities;
using MoqSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample
{
    public class AppointmentsController
    {
        private readonly IMapper mapper;
        private readonly IAppointmentsRepository appointmentsRepository;
        private readonly IClaimsRepository claimsRepository;

        public AppointmentsController(IMapper mapper, IAppointmentsRepository appointmentRepository, IClaimsRepository claimsRepository)
        {
            this.mapper = mapper;
            this.appointmentsRepository = appointmentRepository;
            this.claimsRepository = claimsRepository;
        }

        public Appointment CreateAppointment(Guid claimId, Repairer repairer, DateTime dateTime)
        {
            var claim = claimsRepository.GetById(claimId);

            if (claim == null)
                throw new ArgumentException(Literals.Claims_Id_doesnt_Exist);


            var appointment = new Appointment();
            appointment.Title = $"Appointment on the {dateTime.ToString("dd/MM/yyyy hh:mm")}";
            appointment.Repairer = repairer;
            appointment.Claim = claim;
            appointment.DateAndTime = dateTime;

            claim.Appointments.Add(appointment);

            this.appointmentsRepository.Add(appointment);

            return appointment;
        }

        public void Start(Guid appointmentId)
        {
            var appointment = this.appointmentsRepository.GetById(appointmentId);
            if (appointment == null)
                throw new ArgumentException(nameof(appointmentId));


            appointment.IsStarted = true;
        }

        public void Finish(Guid appointmentId)
        {
            var appointment = this.appointmentsRepository.GetById(appointmentId);
            if (appointment == null)
                throw new ArgumentException(nameof(appointmentId));


            appointment.IsStarted = true;
            appointment.IsFinished = true;
        }

    }
}
