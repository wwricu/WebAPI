using SqlSugar;

namespace WebAPI.Entity
{
    public class StaffOfferingMapping
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int SysUserID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int CourseOfferingID { get; set; }
    }
}
