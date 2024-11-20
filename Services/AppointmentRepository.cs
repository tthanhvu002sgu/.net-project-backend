using DoAn_API.Data;
using DoAn_API.Interfaces;
using DoAn_API.Models;
using DoAn_API.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DoAn_API.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {



        private readonly IDoctorRepository _doctorRepository;
        private readonly MyDbContext _context;
        private readonly IPatientRepository _patientRepository;
        private readonly EmailRepository _emailRepository;

        public AppointmentRepository(MyDbContext context, IDoctorRepository doctorRepository, IPatientRepository patientRepository, EmailRepository emailRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _emailRepository = emailRepository;
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
                     patientName = a.patientName,
                     appointmentFee = a.appointmentFee,
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
                    patientName = a.patientName,
                    appointmentFee = a.appointmentFee,
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
            var patientInfo = await _patientRepository.GetPatientByEmailAsync(appointmentVM.patientEmail);
            // Kiểm tra xem có lịch hẹn nào đã được đặt trùng với giờ, ngày và bác sĩ
            bool isDuplicate = await _context.Appointments.AnyAsync(a =>
                a.doctorId == doctorId &&
                a.date == appointmentVM.date &&
                a.time == appointmentVM.time && a.appointmentStatus != Appointment.Status.Cancelled);

            if (isDuplicate)
            {
                // Trả về thông báo hoặc false nếu trùng
                return false; // Hoặc bạn có thể ném exception để báo lỗi cụ thể
            }
            var appointment = new Appointment
            {
                doctorId = doctorId,
                patientId = patientId,
                patientEmail = appointmentVM.patientEmail,
                doctorEmail = appointmentVM.doctorEmail,
                specialization = doctor.specializationName,
                address = doctorInfo.Address,
                doctorName = doctor.doctorName,
                patientName = patientInfo.FullName,
                appointmentFee = appointmentVM.appointmentFee,
                paymentId = 0, //null
                date = appointmentVM.date,
                time = appointmentVM.time,
                appointmentId = (int)_context.Appointments.Count() + 1,
                appointmentStatus = (Appointment.Status)appointmentVM.appointmentStatus // Assuming matching enum
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return true;
        }
        // Phương thức lấy danh sách các khung giờ đã đặt dựa trên email bác sĩ
        public async Task<List<Appointment>> GetBookedSlotsByDoctorEmailAsync(string doctorEmail)
        {
            return await _context.Appointments
                .Where(a => a.doctorEmail == doctorEmail)
                .Where(a => a.appointmentStatus != Appointment.Status.Cancelled)
                .Select(a => new Appointment
                {
                    date = a.date,
                    time = a.time,
                    appointmentStatus = a.appointmentStatus
                })
                .ToListAsync();
        }
        public async Task<bool> IsPatientDoubleBookingAsync(string patientEmail, string date, string time)
        {
            // Kiểm tra định dạng trước khi so sánh
            if (!Regex.IsMatch(date, @"^\d{4}-\d{2}-\d{2}$") || !Regex.IsMatch(time, @"^\d{2}:\d{2}$"))
            {
                throw new ArgumentException("Invalid date or time format. Expected formats: yyyy-MM-dd for date, HH:mm for time.");
            }

            // Kiểm tra trùng lịch
            var existingAppointment = await _context.Appointments
                .Where(a => a.patientEmail == patientEmail)
                .Where(a => a.date == date && a.time == time)
                .Where(a => a.appointmentStatus != Appointment.Status.Cancelled)
                .FirstOrDefaultAsync();

            return existingAppointment != null; // Trả về true nếu trùng
        }


        // Phương thức từ chối lịch hẹn (yêu cầu lý do)
        public async Task<bool> RejectAppointmentAsync(string doctorEmail, string patientEmail, string date, string time, string rejectionReason)
        {
            // Kiểm tra định dạng trước khi so sánh
            if (!Regex.IsMatch(date, @"^\d{4}-\d{2}-\d{2}$") || !Regex.IsMatch(time, @"^\d{2}:\d{2}$"))
            {
                throw new ArgumentException("Invalid date or time format. Expected formats: yyyy-MM-dd for date, HH:mm for time.");
            }
            var appointment = await _context.Appointments
           .FirstOrDefaultAsync(a =>
               a.patientEmail == patientEmail &&
               a.doctorEmail == doctorEmail &&
               a.date == date &&
               a.time == time && a.appointmentStatus != Appointment.Status.Cancelled);
            if (appointment == null) return false;
            try
            {
                await _emailRepository.SendEmailAsync(
                    appointment.patientEmail,
                    "Your appointment has been rejected",
                    $"Dear {appointment.patientName},\n\nYour appointment with Dr. {appointment.doctorName} on {appointment.date} at {appointment.time} has been rejected.\nReason: {rejectionReason}"
                );
            }
            catch (NullReferenceException ex)
            {
                throw new Exception("Failed to send email. One or more required fields are null.", ex);
            }
            appointment.appointmentStatus = Appointment.Status.Cancelled;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();




            return true;
        }

    }

}
