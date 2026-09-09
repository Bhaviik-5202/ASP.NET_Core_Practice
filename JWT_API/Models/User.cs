namespace JWT_API.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public string phone { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
