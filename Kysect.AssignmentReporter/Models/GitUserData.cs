namespace Kysect.AssignmentReporter.Models
{
    public class GitUserData
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public GitUserData(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
