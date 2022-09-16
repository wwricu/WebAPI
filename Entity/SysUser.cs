using SqlSugar;
using System.Security.Principal;
using WebAPI.Entity;

namespace WebAPI.Entity
{
    public class PublicInfoModel
    {
        [SqlSugar.SugarColumn(ColumnDataType = "varchar(4000)", IsJson = true)]
        public string[]? UserName { get; set; }
        public string? UserNumber { get; set; }
        public string Email { get; set; }
        public int Permission { get; set; }
        public string? Academic { get; set; }
    }

    public class PrivateInfoModel : PublicInfoModel
    {
        [SqlSugar.SugarColumn(ColumnDataType = "varchar(4000)", IsJson = true)]
        public string[]? Addresses { get; set; }
        public string? Phone { get; set; }
        public string? Birthdate { get; set; }
    }

    public class SysUser : PrivateInfoModel
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int SysUserID { get; set; }
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
                  nameof(StudentOfferingMapping.SysUserID),
                  nameof(StudentOfferingMapping.CourseOfferingID))]
        public List<CourseOffering>? CourseOfferingList { get; set; }

        [Navigate(typeof(PrivilegeStaffMapping),
          nameof(PrivilegeStaffMapping.SysUserID),
          nameof(PrivilegeStaffMapping.PrivilegeID))]
        public List<Privilege>? PrivilegeList { get; set; }
    }
}
