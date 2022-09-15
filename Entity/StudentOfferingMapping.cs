using SqlSugar;

namespace WebAPI.Entity
{
    public class StudentOfferingMapping
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int SysUserID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int CourseOfferingID { get; set; }
    }
}
