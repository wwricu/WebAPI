/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 15/09/2022
******************************************/

using SqlSugar;

namespace WebAPI.Entity
{
    public class Privilege
    {
        public Privilege()
        {
            PrivilegeName = "Defaule Privilege Name";
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int PrivilegeID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string PrivilegeName { get; set; }
        [SugarColumn(ColumnDataType = "varchar(1000)", IsNullable = true)]
        public string? Description { get; set; }

        [Navigate(typeof(PrivilegeOfferingMapping),
                  nameof(PrivilegeOfferingMapping.PrivilegeID),
                  nameof(PrivilegeOfferingMapping.CourseOfferingID))]
        public List<CourseOffering>? CourseOfferingList { get; set; }

        [Navigate(typeof(PrivilegeStaffMapping),
          nameof(PrivilegeStaffMapping.PrivilegeID),
          nameof(PrivilegeStaffMapping.SysUserID))]
        public List<SysUser>? StaffList { get; set; }
    }
}
