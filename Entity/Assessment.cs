using SqlSugar;

namespace WebAPI.Entity
{
    public class Assessment
    {
        public Assessment() { } // template
        public Assessment(Assessment template, SysUser student) // instance
        {
            BaseAssessmentID = template.AssessmentID;
            StudentID = student.SysUserID;
            CourseOfferingID = template.CourseOfferingID;
            Name = template.Name;
            Type = template.Type;
            BeginDate = template.BeginDate;
            EndDate = template.EndDate;
            Status = "TO DO";
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int AssessmentID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? BeginDate;
        public string? EndDate;
        public string? Status { get; set; }
        /*
         * For assessment templates, lists of its assessment instances
         */
        public int CourseOfferingID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(CourseOfferingID))]
        public CourseOffering? CourseOffering { get; set; }
        [Navigate(NavigateType.OneToMany, nameof(Assessment.AssessmentID))]
        public List<Assessment>? DerivedAssessmentList { get; set; }
        /* For assessment instance,
         * points to the assessment template
         */
        public int BaseAssessmentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(BaseAssessmentID))]
        public Assessment? BaseAssessment { get; set; }
        public int StudentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StudentID))]
        public SysUser? Student { get; set; }

        public int LocationID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(LocationID))]
        public Location? Location { get; set; }
    }
}

