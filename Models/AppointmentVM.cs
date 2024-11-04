namespace DoAn_API.Models
{
    public class AppointmentVM
    {
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public int paymentId { get; set; }
        public enum Status
        {
            Pending = 0,
            Confirmed = 1,
            Cancelled = 2
        }
        public string appointmentId { get; set; }
        public string appointmentTitle { get; set; }
        public string appointmentDescription { get; set; }
        public Status appointmentStatus { get; set; }
    }
}
