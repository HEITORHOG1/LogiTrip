using LogiTrip.Model.Entity;

namespace LogiTrip.Model.Responses
{
    public class UserResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        public User User { get; set; }
        public List<string> Role { get; set; }
    }
}