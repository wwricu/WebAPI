using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    public class Application
    {
        public Application() { }
        public Application(AssessmentInstance assessment)
        {
            InstanceID = assessment.AssessmentID;
            StudentID = assessment.StudentID;
            Type = "Formal Exam";
            Status = "Draft";
            SubmitDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int ApplicationID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Type { get; set; }
        [SugarColumn(ColumnDataType = "varchar(10000)")]
        public string? Documentation { get; set; }
        public string? Descriptipn { get; set; }
        public string? SubmitDate { get; set; }
        public string? Status { get; set; } // Draft, Pending, Approved, Rejected
        public int StudentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StudentID))]
        public SysUser? Student { get; set; }
        public int StaffID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StaffID))]
        public SysUser? Staff { get; set; }
        public string? InstanceID { get; set; } // instance
        [Navigate(NavigateType.OneToOne, nameof(InstanceID))]
        public AssessmentInstance? AssessmentInstance { get; set; }
    }
}
