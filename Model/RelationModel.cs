using WebAPI.Entity;

namespace WebAPI.Model
{
    public class RelationModel
    {
        public SysUser? User { get; set; }
        public int permission { get; set; }
        public List<CourseOffering>? CourseAddList { get; set; }
        public List<CourseOffering>? CourseRemoveList { get; set; }
        public CourseOffering? CourseOffering { get; set; }
        public List<SysUser>? UserAddList { get; set; }
        public List<SysUser>? UserRemoveList { get; set; }
    }
}
