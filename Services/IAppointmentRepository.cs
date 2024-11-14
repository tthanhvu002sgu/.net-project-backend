using DoAn_API.Models;

namespace DoAn_API.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> AddAppointmentAsync(AppointmentVM appointment);
        Task<List<AppointmentVM>> GetAppointmentsByPatientEmail(string patientEmail);
        Task<List<AppointmentVM>> GetAppointmentsByDoctorEmail(string doctorEmail);
    }
}
