using SqlSugar;

namespace WebAPI.Entity
{
    public class Assessment
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int AssessmentID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? BeginDate { get; set; }
        public string? EndDate { get; set; }
        public int CourseOfferingID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(CourseOfferingID))]
        public CourseOffering? CourseOffering { get; set; }
        public int LocationID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(LocationID))]
        public Location? Location { get; set; }
    }
    public class AssessmentTemplate : Assessment
    {
        [Navigate(NavigateType.OneToMany, nameof(Assessment.AssessmentID))]
        public List<Assessment>? DerivedAssessmentList { get; set; }
    }
    public class AssessmentInstance : Assessment
    {
        public AssessmentInstance() { }
        public AssessmentInstance(AssessmentTemplate assessment,
                                  SysUser student)
        {
            Name = assessment.Name;
            Type = assessment.Type;
            Description = assessment.Description;
            BeginDate = assessment.BeginDate;
            EndDate = assessment.EndDate;
            StudentID = student.SysUserID;
            BaseAssessmentID = assessment.AssessmentID;
            Status = "TO DO";
        }
        public int BaseAssessmentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(BaseAssessmentID))]
        public Assessment? BaseAssessment { get; set; }
        public int StudentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StudentID))]
        public SysUser? Student { get; set; }
        public string? Status { get; set; }
    }
}

