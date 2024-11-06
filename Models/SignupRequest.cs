namespace DoAn_API.Models
{
    public class SignupRequest
    {
        public SignUpVM model { get; set; }
        public DoctorVM? doctorVM { get; set; }
    }
}
