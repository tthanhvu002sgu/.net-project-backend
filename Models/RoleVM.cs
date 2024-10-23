namespace DoAn_API.Models
{
    public class RoleVM
    {
        public int roleId { get; set; }
        public string roleName { get; set; }

        public ICollection<UserVM> users { get; set; }
        public RoleVM(int id, string name)
        {
            roleId = id;
            roleName = name;
        }
        public RoleVM()
        {
            users = new List<UserVM>();
        }
    }
}
