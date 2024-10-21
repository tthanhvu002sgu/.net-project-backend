namespace DoAn_API.Data
{
    public class Payment
    {
        public int paymentId { get; set; }
        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
        public Appointment appointment { get; set; }

    }
}
