using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
            Status = "Draft";
            SubmitDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        [SugarColumn(IsPrimaryKey = true)]
        [Newtonsoft.Json.JsonConverter(typeof(ValueToStringConverter))]
        public long ApplicationID { get; set; }
        
        [SugarColumn(IsNullable = true)]
        public string? Reason { get; set; }
        
        [SugarColumn(IsNullable = true)]
        public string? DaysOfImpact { get; set; } // days of impact
        
        [SugarColumn(ColumnDataType = "varchar(8000)", IsNullable = true)]
        public string? CircumstanceDetail { get; set; }
        
        [Navigate(NavigateType.OneToMany, nameof(Document.ApplicationID))]
        public List<Document>? DocumentList { get; set; }
        

        [SugarColumn(IsNullable = true)]
        public string? Outcome { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? OutcomeDetail { get; set; }

        public string? Status { get; set; } // Draft, Pending, Approved, Rejected
        public string? SubmitDate { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? StaffComment { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? StudentNumber { get; set; }
        [SugarColumn(IsNullable = true)]
        public int StudentID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StudentID))]
        public SysUser? Student { get; set; }
        [SugarColumn(IsNullable = true)]
        public int StaffID { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(StaffID))]
        public SysUser? Staff { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? InstanceID { get; set; } // instance
        [Navigate(NavigateType.OneToOne, nameof(InstanceID))]
        public AssessmentInstance? AssessmentInstance { get; set; }
    }
}
