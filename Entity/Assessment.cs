using SqlSugar;

namespace WebAPI.Entity
{
    public class Assessment
    {
        public Assessment() { }
        public Assessment(string uuid) { AssessmentID = uuid; }
        [SugarColumn(IsPrimaryKey = true)]
        public string? AssessmentID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? CourseOfferingName { get; set; }
        public string? BeginDate { get; set; }
        public string? BeginTime { get; set; }
        public string? EndDate { get; set; }
        public string? EndTime { get; set; }
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
        public AssessmentInstance(AssessmentTemplate template,
                                  SysUser student)
        {
            Name = template.Name;
            Type = template.Type;
            CourseOfferingID = template.CourseOfferingID;
            CourseOfferingName = template.CourseOfferingName;
            BeginDate = template.BeginDate;
            BeginTime = template.BeginTime;
            EndDate = template.EndDate;
            EndTime = template.EndTime;

            AssessmentID = Guid.NewGuid().ToString();
            LocationID = template.LocationID;
            StudentID = student.SysUserID;
            BaseAssessmentID = template.AssessmentID;
            Status = "TO BE COMPLETED"; // TO BECOMPLETE, EXEMPTED, COMPLETED
        }
        public string? BaseAssessmentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(BaseAssessmentID))]
        public Assessment? BaseAssessment { get; set; }
        public int StudentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StudentID))]
        public SysUser? Student { get; set; }
        public string? Status { get; set; }
    }
}

