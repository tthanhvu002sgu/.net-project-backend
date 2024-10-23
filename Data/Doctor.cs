namespace DoAn_API.Data
{
    public class Doctor : User
    {
        public string degree { get; set; }
        public double experience { get; set; }
        public double bookingFee { get; set; }
        public string doctorAbout { get; set; }

        //relationship

        public ICollection<Appointment> appointments { get; set; }
        public ICollection<Schedule> schedules { get; set; }
        public ICollection<Specialization> specializations { get; set; }
        public Doctor() : base()
        {
            specializations = new List<Specialization>();
            schedules = new List<Schedule>();
            appointments = new List<Appointment>();
        }
    }
}
