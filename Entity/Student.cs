using SqlSugar;

namespace WebAPI.Entity
{
    public class Student : SysUser
    {
        public Student() { AcademicProgram = "Default Program"; }
        public Student(SysUser sysUser)
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
            AcademicProgram = "Default Program";
        }
        public string? AcademicProgram { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
          nameof(StudentOfferingMapping.SysUserID),
          nameof(StudentOfferingMapping.CourseOfferingID))]
        public List<CourseOffering>? CourseOfferingList { get; set; }
    }
}
