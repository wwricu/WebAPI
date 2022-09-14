using SqlSugar;

namespace WebAPI.Entity
{
    public class PrivilegeStaffMapping
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int PrivilegeID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int StaffID { get; set; }
    }
}
