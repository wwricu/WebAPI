using SqlSugar;

namespace WebAPI.Entity
{
    public class PrivilegeOfferingMapping
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int PrivilegeID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int CourseOfferingID { get; set; }
    }
}
