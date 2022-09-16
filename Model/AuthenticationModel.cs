namespace WebAPI.Model
{
    public class PasswordLoginModel
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }

    public class TokenLoginModel
    {
        public string? Token { get; set; }
    }
}
