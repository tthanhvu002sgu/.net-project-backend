namespace DoAn_API.Models
{
    public class SpecializationVM
    {
        public string specialization { get; set; }
        public int specializationId { get; set; }
        public ICollection<DoctorVM> doctors { get; set; }
        public SpecializationVM()
        {
            doctors = new List<DoctorVM>();
        }
    }
}
