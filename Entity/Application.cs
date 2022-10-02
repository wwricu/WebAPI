using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    public class Application
    {
        public Application(AssessmentInstance assessment)
        {
            Type = "Formal Exam";
            Status = "Pending";
            AssessmentID = assessment.AssessmentID;
            StudentID = assessment.StudentID;
            SubmitDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int ApplicationID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Type { get; set; }
        [SugarColumn(ColumnDataType = "varchar(10000)")]
        public string? Documentation { get; set; }
        public string? Descriptipn { get; set; }
        public string SubmitDate { get; set; }
        public string Status { get; set; } // Pending, NMI, Approved, Rejected
        public int StudentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StudentID))]
        public SysUser? Student { get; set; }
        public string? AssessmentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(AssessmentID))]
        public Assessment? Assessment { get; set; }
    }
}
