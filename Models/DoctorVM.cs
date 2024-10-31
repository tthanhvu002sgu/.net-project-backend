namespace DoAn_API.Models
{
    public class DoctorVM
    {
        public string email { get; set; }
        public string doctorId { get; set; }
        public string degree { get; set; }
        public double experience { get; set; }
        public double bookingFee { get; set; }
        public string doctorAbout { get; set; }

        //relationship

        public ICollection<AppointmentVM> appointments { get; set; }
        public ICollection<ScheduleVM> schedules { get; set; }
        public ICollection<SpecializationVM> specializations { get; set; }
        public DoctorVM()
        {
            specializations = new List<SpecializationVM>();
            schedules = new List<ScheduleVM>();
            appointments = new List<AppointmentVM>();
        }
    }
}
