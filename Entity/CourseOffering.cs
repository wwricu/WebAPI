using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    public class CourseOffering
    {
        public CourseOffering()
        {
            CourseName = "Default Name";
            Semester = "Trimester 2";
            Year = "2022";
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int CourseOfferingID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string CourseName { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Semester { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Year { get; set; }
        public DateTime beginDate;
        public DateTime endDate;

        [Navigate(typeof(PrivilegeOfferingMapping),
                  nameof(PrivilegeOfferingMapping.CourseOfferingID),
                  nameof(PrivilegeOfferingMapping.PrivilegeID))]
        public List<Privilege>? PrivilegeList { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
                  nameof(StudentOfferingMapping.CourseOfferingID),
                  nameof(StudentOfferingMapping.SysUserID))]
        public List<CourseOffering>? StudentList { get; set; }

        public int AssessmentID { get; set; }
        [Navigate(NavigateType.OneToMany, nameof(Assessment.AssessmentID))]//BookA表中的studenId
        public List<Assessment> AssessmentList { get; set; }//注意禁止给books手动赋值
    }
}
