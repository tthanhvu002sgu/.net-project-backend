namespace DoAn_API.Models
{
    public class Patient : User
    {


        //relationship
        public ICollection<Appointment> appointments { get; set; }
        public Patient() : base()
        {
            appointments = new List<Appointment>();
            roles.Add(new Role(1, "Patient"));
        }


    }
}
