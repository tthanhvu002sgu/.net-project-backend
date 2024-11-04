namespace DoAn_API.Models
{
    public class DoctorVM
    {
        public int specializationId { get; set; }
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
        }
    }
}
