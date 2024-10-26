namespace DoAn_API.Data
{
    public class Role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }

        public ICollection<Doctor> doctors { get; set; }

        public ICollection<Patient> patients { get; set; }
        //public Role(int id, string name)
        //{
        //    roleId = id;
        //    roleName = name;
        //}

    }
}
