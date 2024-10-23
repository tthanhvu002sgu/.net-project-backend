using DoAn_API.Models;

public class UserVM
{
    public string email { get; set; }
    public string image { get; set; }
    public string password { get; set; }
    public string phoneNumber { get; set; }
    public string fullName { get; set; }
    public DateTime dob { get; set; }
    public string gender { get; set; }
    public string address { get; set; }
    public int userId { get; set; }
    public ICollection<RoleVM> roles { get; set; }
    public UserVM()
    {
        roles = new List<RoleVM>();
    }
}