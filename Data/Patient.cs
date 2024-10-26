namespace DoAn_API.Data
{
    public class Patient
    {

        public int userId { get; set; }

        public string email { get; set; }
        public string image { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string fullName { get; set; }
        public DateTime dob { get; set; }
        public string gender { get; set; }
        public string address { get; set; }

        //relationship
        public ICollection<Appointment> appointments { get; set; }
        public ICollection<Role> roles { get; set; }
        //public Patient() : base()
        //{
        //    appointments = new List<Appointment>();
        //    roles.Add(new Role(1, "Patient"));
        //}


    }
}
