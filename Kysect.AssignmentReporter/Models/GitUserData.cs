namespace Kysect.AssignmentReporter.Models
{
    public class GitUserData
    {
        public string Username { get; }
        public string Email { get; }

        public GitUserData(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
