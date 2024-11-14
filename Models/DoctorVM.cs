namespace DoAn_API.Models
{
    public class DoctorVM
    {
        public string specializationName { get; set; }
        public string doctorName { get; set; }
        public string doctorImg { get; set; }
        public string email { get; set; }
        public string degree { get; set; }
        public double experience { get; set; }
        public double bookingFee { get; set; }
        public string doctorAbout { get; set; }
        public bool isAvailable { get; set; }

        //relationship


    }
}
