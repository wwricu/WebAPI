namespace WebAPI.Model
{
    public class PublicInfoModel
    {
        public string[]? UserName { get; set; }
        public string? UserNumber { get; set; }
        public string Email { get; set; }
        public int Permission { get; set; }
        public string? Academic { get; set; }
    }

    public class PrivateInfoModel : PublicInfoModel
    {
        public string[]? Addresses { get; set; }
        public string? Phone { get; set; }
        public string? Birthdate { get; set; }
    }

    public class CredentialInfoModel : PrivateInfoModel
    {
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
    }
}
