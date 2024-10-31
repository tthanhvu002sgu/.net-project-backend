using DoAn_API.Data;

namespace DoAn_API.Models
{
    public class AppointmentVM
    {
        public enum Status
        {
            Pending = 0,
            Confirmed = 1,
            Cancelled = 2
        }
        public string appointmentId { get; set; }
        public string appointmentTitle { get; set; }
        public string appointmentDescription { get; set; }
        public Doctor doctor { get; set; }
        public Status appointmentStatus { get; set; }
        public Patient patient { get; set; }
        public Schedule schedule { get; set; }
        public Payment payment { get; set; }
    }
}
