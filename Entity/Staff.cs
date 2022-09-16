using SqlSugar;

namespace WebAPI.Entity
{
    public class Staff : SysUser
    {
        public Staff() { Faculty = "Default Faculty"; }
        public Staff(SysUser sysUser)
        {
            SysUserID = sysUser.SysUserID;
            PasswordHash = sysUser.PasswordHash;
            Salt = sysUser.Salt;
            Permission = sysUser.Permission;
            MemberNumber = sysUser.MemberNumber;
            Firstname = sysUser.Firstname;
            Middlename = sysUser.Middlename;
            Lastname = sysUser.Lastname;
            Birthdate = sysUser.Birthdate;
            AddressLine1 = sysUser.AddressLine1;
            AddressLine2 = sysUser.AddressLine2;
            AddressLine3 = sysUser.AddressLine3;
            Phone = sysUser.Phone;
            Email = sysUser.Email;
            Faculty = "Default Faculty";
        }
        public string? Faculty { get; set; }

        [Navigate(typeof(PrivilegeStaffMapping),
                  nameof(PrivilegeStaffMapping.SysUserID),
                  nameof(PrivilegeStaffMapping.PrivilegeID))]
        public List<Privilege>? PrivilegeList { get; set; }
    }

}
