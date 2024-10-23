namespace DoAn_API.Models
{
    public class PatientVM : UserVM
    {


        //relationship
        //public ICollection<AppointmentVM> appointments { get; set; }
        public PatientVM() : base()
        {
            // appointments = new List<AppointmentVM>();
            roles.Add(new RoleVM(1, "Patient"));
        }


    }
}
