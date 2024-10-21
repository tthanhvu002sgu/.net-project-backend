namespace DoAn_API.Models
{
    public class Specialization
    {
        public string specialization { get; set; }
        public int specializationId { get; set; }
        public ICollection<Doctor> doctors { get; set; }
        public Specialization()
        {
            doctors = new List<Doctor>();
        }
    }
}
