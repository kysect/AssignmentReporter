namespace Kysect.AssignmentReporter.Models
{
    public class GitUserData
    {
        public GitUserData(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public string Username { get; set; }
        public string Email { get; set; }
    }
}