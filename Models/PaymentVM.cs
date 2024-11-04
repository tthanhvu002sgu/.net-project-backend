namespace DoAn_API.Models
{
    public class PaymentVM
    {

        public int patientId { get; set; }
        public int appointmentId { get; set; }
        public string paymentId { get; set; }

        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
    }
}
