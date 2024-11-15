namespace DoAn_API.Models
{
    public class PaymentVM
    {

        public int? patientId { get; set; } //get patient by email
        public string patientEmail { get; set; }
        public int appointmentId { get; set; } //get appointment by patient email
        public int paymentId { get; set; } //_context.Payments.count()
        public decimal price { get; set; } // price = doctorfee
        public string paymentMethod { get; set; } //cash, transfer
        public string paymentStatus { get; set; } //success, fail, pending
    }
}
