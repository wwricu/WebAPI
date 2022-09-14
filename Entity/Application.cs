using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    public class Application
    {
        public Application(CourseOffering courseOffering)
        {
            Type = "Formal Exam";
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int ApplicationID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Type { get; set; }
        [SugarColumn(ColumnDataType = "varchar(10000)")]
        public string? Documentation { get; set; }
        public string? Descriptipn { get; set; }
        public DateTime SubmitDate;
        public int Status { get; set; }

        public int StudentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StudentID))]
        public Student? Student { get; set; }

        public int AssessmentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(AssessmentID))]
        public Assessment? Assessment { get; set; }
    }
}
