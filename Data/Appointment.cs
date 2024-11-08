namespace DoAn_API.Data
{
    public class Appointment
    {
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public int paymentId { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
        public enum Status
        {
            Pending = 0,
            Confirmed = 1,
            Cancelled = 2
        }
        public int appointmentId { get; set; }
        public string doctorEmail { get; set; }
        public string appointmentTitle { get; set; }
        public string appointmentDescription { get; set; }
        public Status appointmentStatus { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
        public Payment payment { get; set; }


    }
}
