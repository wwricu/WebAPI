using WebAPI.Entity;

namespace WebAPI.Model
{
    public class AssessmentModel
    {
        public SysUser? Student { get; set; } 
        public CourseOffering? CourseOffering { get; set; }
        public Assessment? Assessment { get; set; }
    }
}
