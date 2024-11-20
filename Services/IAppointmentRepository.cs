using DoAn_API.Data;
using DoAn_API.Models;

namespace DoAn_API.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> AddAppointmentAsync(AppointmentVM appointment);
        Task<List<AppointmentVM>> GetAppointmentsByPatientEmail(string patientEmail);
        Task<List<AppointmentVM>> GetAppointmentsByDoctorEmail(string doctorEmail);
        Task<List<Appointment>> GetBookedSlotsByDoctorEmailAsync(string doctorEmail);
        Task<bool> IsPatientDoubleBookingAsync(string patientEmail, string date, string time);
        Task<bool> RejectAppointmentAsync(string doctorEmail, string patientEmail, string date, string time, string rejectionReason);
    }
}
