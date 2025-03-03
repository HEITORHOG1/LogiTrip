using LogiTrip.Model.Entity;

namespace LogiTrip.Model.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        public List<string> Roles { get; set; }

        public User User { get; set; }

        public void AssignRolesToUser()
        {
            if (User != null && Roles != null)
            {
                User.Role = Roles.FirstOrDefault();
            }
        }
    }
}