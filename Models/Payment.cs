namespace DoAn_API.Models
{
    public class Payment
    {
        public int UserId {  get; set; }
        public int AppointmentId {  get; set; }
        public int PaymentId {  get; set; }
        public string PaymentMethod {  get; set; }
        public string PaymentStatus { get; set; }
    }
}
