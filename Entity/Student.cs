using SqlSugar;


namespace WebAPI.Entity
{
    public class Student
    {
        public string? Address { get; set; }

        public string? StudentNumber { get; set; }
        public string? StudentName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AcademicProgram { get; set; }
        public int Status { get; set; }

        [Navigate(typeof(StudentOfferingMapping),
          nameof(StudentOfferingMapping.StudentID),
          nameof(StudentOfferingMapping.CourseOfferingID))]
        public List<CourseOffering>? CourseOfferingList { get; set; }
    }
}
