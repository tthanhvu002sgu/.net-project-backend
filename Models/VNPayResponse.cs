namespace DoAn_API.Models
{
    public class VNPayResponse
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }

    }

    public class VNPayRequest()
    {
        public int OrderId { get; set; }
        public string Fullname { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int AppointmentId { get; set; }

        public string userId { get; set; }

        public DateTime CreatedDate { get; set; }
    }

}
