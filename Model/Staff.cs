/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 17/09/2022
******************************************/

using SqlSugar;
using WebAPI.Model;

namespace WebAPI.Entity
{
    public class Staff : SysUser
    {
        public Staff() { Faculty = "Default Faculty"; }
        public Staff(CredentialInfoModel sysUser)
        {
            PasswordHash = sysUser.PasswordHash;
            Salt = sysUser.Salt;
            Permission = sysUser.Permission;
            MemberNumber = sysUser.UserNumber;
            Firstname = sysUser.UserName[0];
            Middlename = sysUser.UserName[1];
            Lastname = sysUser.UserName[2];
            Birthdate = sysUser.Birthdate;
            AddressLine1 = sysUser.Addresses[0];
            AddressLine2 = sysUser.Addresses[1];
            AddressLine3 = sysUser.Addresses[2];
            Phone = sysUser.Phone;
            Email = sysUser.Email;
            Faculty = sysUser.Academic;
        }
        public string? Faculty { get; set; }

        [Navigate(typeof(PrivilegeStaffMapping),
                  nameof(PrivilegeStaffMapping.SysUserID),
                  nameof(PrivilegeStaffMapping.PrivilegeID))]
        public List<Privilege>? PrivilegeList { get; set; }
    }

}
