namespace DoAn_API.Data
{
    public class Appointment
    {
        public int doctorId { get; set; }
        public int patientId { get; set; }
        public string patientEmail { get; set; }
        public string doctorEmail { get; set; }
        public string specialization { get; set; }
        public string address { get; set; }
        public string doctorName { get; set; }
        public string patientName { get; set; }
        public decimal appointmentFee { get; set; }
        public int paymentId { get; set; }

        public string date { get; set; }
        public string time { get; set; }
        public enum Status
        {
            Pending = 0,
            Confirmed = 1,
            Cancelled = 2
        }
        public int appointmentId { get; set; }
        public Status appointmentStatus { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
        public Payment payment { get; set; }


    }
}
