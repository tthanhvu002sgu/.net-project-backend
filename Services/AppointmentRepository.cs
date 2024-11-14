using DoAn_API.Data;
using DoAn_API.Interfaces;
using DoAn_API.Models;
using DoAn_API.Services;
using Microsoft.EntityFrameworkCore;

namespace DoAn_API.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly MyDbContext _context;
        private readonly IPatientRepository _patientRepository;

        public AppointmentRepository(MyDbContext context, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _context = context;
        }
        public async Task<List<AppointmentVM>> GetAppointmentsByPatientEmail(string patientEmail)
        {
            var appointments = await _context.Appointments
                 .Where(a => a.patientEmail == patientEmail)
                 .Select(a => new AppointmentVM
                 {
                     patientEmail = a.patientEmail,
                     doctorEmail = a.doctorEmail,
                     specialization = a.specialization,
                     address = a.address,
                     doctorName = a.doctorName,
                     paymentId = 1,
                     date = a.date,
                     time = a.time,
                     appointmentStatus = (AppointmentVM.Status)(int)a.appointmentStatus
                 })
                 .ToListAsync();
            return appointments;
        }
        public async Task<List<AppointmentVM>> GetAppointmentsByDoctorEmail(string doctorEmail)
        {
            var appointments = await _context.Appointments
                .Where(a => a.doctorEmail == doctorEmail)
                .Select(a => new AppointmentVM
                {
                    patientEmail = a.patientEmail,
                    doctorEmail = a.doctorEmail,
                    specialization = a.specialization,
                    address = a.address,
                    doctorName = a.doctorName,
                    paymentId = 1,
                    date = a.date,
                    time = a.time,
                    appointmentStatus = (AppointmentVM.Status)(int)a.appointmentStatus
                })
                .ToListAsync();
            return appointments;
        }
        public async Task<bool> AddAppointmentAsync(AppointmentVM appointmentVM)
        {
            var doctorId = await _doctorRepository.GetDoctorIdByEmail(appointmentVM.doctorEmail);
            var patientId = await _patientRepository.GetPatientIdByEmail(appointmentVM.patientEmail);
            var doctor = await _doctorRepository.GetDoctorByEmail(appointmentVM.doctorEmail);
            var doctorInfo = await _doctorRepository.GetDoctorInfoByEmail(appointmentVM.doctorEmail);
            var appointment = new Appointment
            {
                doctorId = doctorId,
                patientId = patientId,
                patientEmail = appointmentVM.patientEmail,
                doctorEmail = appointmentVM.doctorEmail,
                specialization = doctor.specializationName,
                address = doctorInfo.Address,
                doctorName = doctor.doctorName,
                paymentId = 1,
                date = appointmentVM.date,
                time = appointmentVM.time,
                appointmentId = _context.Appointments.Count() + 1,
                appointmentStatus = (Appointment.Status)appointmentVM.appointmentStatus // Assuming matching enum
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
