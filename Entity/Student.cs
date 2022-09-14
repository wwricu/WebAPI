using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using SqlSugar;


namespace WebAPI.Entity
{
    public class Student : IdentityUser
    {
        [Required]
        [StringLength(128)]
        public string? Address { get; set; }

        [PersonalData]
        public string? StudentNumber { get; set; }
        [PersonalData]
        public string? StudentName { get; set; }
        [PersonalData]
        public DateTime BirthDate { get; set; }
        [PersonalData]
        public string? AddressLine1 { get; set; }
        [PersonalData]
        public string? AddressLine2 { get; set; }
        [PersonalData]
        public string? AddressLine3 { get; set; }
        [PersonalData]
        public string? AcademicProgram { get; set; }
        [PersonalData]
        public int Status { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
          nameof(StudentOfferingMapping.StudentID),
          nameof(StudentOfferingMapping.CourseOfferingID))]
        public List<CourseOffering>? CourseOfferingList { get; set; }
    }
}
