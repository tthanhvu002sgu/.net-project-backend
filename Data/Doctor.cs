namespace DoAn_API.Data
{
    public class Doctor
    {
        public string specializationName { get; set; }
        public int specializationId { get; set; }
        public int doctorId { get; set; }
        public string doctorName { get; set; }
        public string doctorImage { get; set; }
        public string email { get; set; }
        public string? degree { get; set; }
        public double? experience { get; set; }
        public double? bookingFee { get; set; }
        public string? doctorAbout { get; set; }

        public bool isAvailable { get; set; }


        //relationship

        public ICollection<Appointment> appointments { get; set; }
        public Specialization specialization { get; set; }

        public Doctor() : base()
        {
            appointments = new List<Appointment>();
        }
    }
}
