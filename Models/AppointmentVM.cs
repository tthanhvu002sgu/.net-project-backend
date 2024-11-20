namespace DoAn_API.Models
{
    public class AppointmentVM
    {
        public string patientEmail { get; set; }
        public string doctorEmail { get; set; }
        public string specialization { get; set; }
        public string address { get; set; }
        public string doctorName { get; set; }
        public string? patientName { get; set; }
        public decimal appointmentFee { get; set; }
        public int paymentId { get; set; }
        public enum Status
        {
            Pending = 0,
            Confirmed = 1,
            Cancelled = 2
        }
        public string date { get; set; }
        public string time { get; set; }
        public int? appointmentId { get; set; }

        public Status appointmentStatus { get; set; }
    }
}
