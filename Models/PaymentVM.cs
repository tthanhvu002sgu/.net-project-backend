namespace DoAn_API.Models
{
    public class PaymentVM
    {
        public string paymentId { get; set; }
        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
        public AppointmentVM appointment { get; set; }
    }
}
