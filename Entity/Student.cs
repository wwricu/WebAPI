using SqlSugar;

namespace WebAPI.Entity
{
    public class Student
    {
        public string? AcademicProgram { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
          nameof(StudentOfferingMapping.SysUserID),
          nameof(StudentOfferingMapping.CourseOfferingID))]
        public List<CourseOffering>? CourseOfferingList { get; set; }
    }
}
