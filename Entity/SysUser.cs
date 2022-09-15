namespace WebAPI.Entity
{
    public class SysUser
    {
        public int SysUserID { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        /*
         -1: invalid, 0: Student, 1: staff, 2: Admin, 3: Super User
         */
        public int Permission { get; set; }

        public string MemberNumber { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }

        public DateTime Birthdate { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
