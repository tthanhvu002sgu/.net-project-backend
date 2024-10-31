namespace DoAn_API.Data
{
    public class Patient
    {
        public int patientId { get; set; }
        public string email { get; set; }

        //relationship
        public ICollection<Appointment> appointments { get; set; }



    }
}
