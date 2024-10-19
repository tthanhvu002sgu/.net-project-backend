namespace DoAn_API.Data
{
    public class Appointment
    {
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public int appointmentId { get; set; }
        public string status { get; set; }
        public string appointmentTitle { get; set; }
        public string appointmentDescription { get; set; }
        public DateTime appointmentDate { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
        public Schedule schedule { get; set; }



    }
}
