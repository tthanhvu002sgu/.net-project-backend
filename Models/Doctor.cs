using System.Security.Cryptography.X509Certificates;

namespace DoAn_API.Models
{
    public class Doctor
    {
        public int doctorId { get; set; }

        public string doctorName { get; set; }
        public string degree { get; set; }
        public double experience { get; set; }
        public double bookingFee {  get; set; }
        public List<string> speciality { get; set; } 
    }
}
