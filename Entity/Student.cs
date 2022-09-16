using SqlSugar;
using WebAPI.Model;

namespace WebAPI.Entity
{
    public class Student : SysUser
    {
        public Student() { AcademicProgram = "Default Program"; }
        public Student(CredentialInfoModel sysUser)
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
            AcademicProgram = sysUser.Academic;
        }
        public string? AcademicProgram { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
          nameof(StudentOfferingMapping.SysUserID),
          nameof(StudentOfferingMapping.CourseOfferingID))]
        public List<CourseOffering>? CourseOfferingList { get; set; }
    }
}
