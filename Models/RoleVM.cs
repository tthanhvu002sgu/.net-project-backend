namespace DoAn_API.Models
{
    public class Role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }

        public ICollection<User> users { get; set; }
        public Role(int id, string name)
        {
            roleId = id;
            roleName = name;
        }
        public Role()
        {
            users = new List<User>();
        }
    }
}
