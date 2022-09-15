using SqlSugar;

namespace WebAPI.Entity
{
    public class Staff
    {
        public string? Faculty { get; set; }

        [Navigate(typeof(PrivilegeStaffMapping),
                  nameof(PrivilegeStaffMapping.SysUserID),
                  nameof(PrivilegeStaffMapping.PrivilegeID))]
        public List<Privilege>? PrivilegeList { get; set; }
    }

}
