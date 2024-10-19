namespace DoAn_API.Data
{
    public class Patient : User
    {
        public int patientId { get; set; }


        //relationship
        public ICollection<Appointment> appointments { get; set; }
        public Patient()
        {
            this.role = 0;
            appointments = new List<Appointment>();
        }


    }
}
