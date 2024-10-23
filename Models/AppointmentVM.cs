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
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public int appointmentId { get; set; }
        public string appointmentTitle { get; set; }
        public string appointmentDescription { get; set; }
        public DoctorVM doctor { get; set; }
        public Status appointmentStatus { get; set; }
        public PatientVM patient { get; set; }
        public ScheduleVM schedule { get; set; }
        public PaymentVM payment { get; set; }

    }
}
