namespace DoAn_API.Models
{
    public class Appointment
    {
        public int userId { get; set; }
        public int doctorId {  get; set; }
        public int appointmentId { get; set; }
        public string status { get; set; }
        public string appointmentTitle {  get; set; }
        public string appointmentDescription { get; set; }
        public DateTime appointmentDate { get; set; }
        
    }
}
