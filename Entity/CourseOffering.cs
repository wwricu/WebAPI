/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Jiaming Ta
  Date   : 15/09/2022
******************************************/

using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    public class CourseOffering
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int CourseOfferingID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? CourseName { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Semester { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Year { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? BeginDate { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? EndDate { get; set; }

        [Navigate(typeof(StaffOfferingMapping),
                  nameof(StaffOfferingMapping.CourseOfferingID),
                  nameof(StaffOfferingMapping.SysUserID))]
        public List<SysUser>? StaffList { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
                  nameof(StudentOfferingMapping.CourseOfferingID),
                  nameof(StudentOfferingMapping.SysUserID))]
        public List<SysUser>? StudentList { get; set; }
        /* Assessment templates (default location and time)
         * will be deleted with CourseOffering
        */
        [Navigate(NavigateType.OneToMany, nameof(Assessment.AssessmentID))]
        public List<Assessment>? AssessmentList { get; set; }
    }
}
