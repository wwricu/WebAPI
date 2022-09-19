using SqlSugar;

namespace WebAPI.Entity
{
    public class Assessment
    {
        public Assessment()
        {
            Type = "Formal Exam";
            Name = "Default Name";
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int AssessmentID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Name { get; set; }
        public string Type { get; set; }
        public string? beginDate;
        public string? endDate;
        public int Status { get; set; }
        
        public int CourseOfferingID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(CourseOfferingID))]
        public CourseOffering? CourseOffering { get; set; }

        public int LocationID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(LocationID))]
        public Location? Location { get; set; }
    }
}

